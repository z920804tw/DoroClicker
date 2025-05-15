using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Upgrade : MonoBehaviour
{
    [Header("物件參考")]
    public TMP_Text incomeText;
    public TMP_Text priceText;
    public TMP_Text itemNameText;
    [SerializeField] Image itemImg;
    public GameObject blockUI;
    GameManager gameManager;
    Button current;
    [Header("數值設定")]
    public UpgradeSO upgradeSO;
    float startPrice;    //開始價錢
    float prevIncome;
    public float PrevIncome { get { return prevIncome; } }
    float upgradePriceMutiplier; //價格倍率
    float upgradePerCountMutiplier; //生產倍率
    float upgradePerTime;
    public int currentCount = 0;  //需要紀錄
    bool unlocked;



    // Start is called before the first frame update
    void Start()
    {
        current = GetComponent<Button>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();


        InitializationInfo();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        bool canBuy = gameManager.CoinCount >= CaculatePrice();
        current.interactable = canBuy;
        blockUI.SetActive(!canBuy);
    }


    public void BuyUpgrade()
    {
        bool canBuy = gameManager.CanBuy(CaculatePrice());

        if (canBuy)
        {
            prevIncome = CaculateIncomePerSecond(); //先更新原本的價格
            currentCount++; //增加數量
            UpdateUI(); //更新UI

            upgradeSO.BuyUpgrade(this); //購買

            if (!unlocked)  //第一次購買後解鎖圖片與文字
            {
                unlocked = true;
                itemImg.color = Color.white;
                itemNameText.text = upgradeSO.itemName;
            }
        }
    }

    public void UpdateUI()
    {
        priceText.text = $"{gameManager.NumberConvert(CaculatePrice())}"; //價錢
        incomeText.text = $"x{currentCount} {gameManager.NumberConvert(CaculateIncomePerSecond())}/s"; //數量和每秒產出量
    }
    int CaculatePrice() //計算購買價格
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMutiplier, currentCount));
        return price;
    }

    public float CaculateIncomePerSecond() //計算每秒產出
    {
        if (currentCount == 0)
            return 0;

        float count = Mathf.Round(upgradePerTime * Mathf.Pow(upgradePerCountMutiplier, currentCount));
        return count;
    }

    public void Reset()
    {
        GameManager gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        unlocked = false;
        itemImg.color = Color.black;
        itemNameText.text = "????";

        priceText.text = $"{gm.NumberConvert(upgradeSO.startPrice)}";

        incomeText.text = $"x0 0/s"; //數量和每秒產出量

    }
    void InitializationInfo()
    {
        itemNameText.text = upgradeSO.itemName;
        itemImg.sprite = upgradeSO.itemImg;
        startPrice = upgradeSO.startPrice;
        upgradePriceMutiplier = upgradeSO.upgradePriceMutiplier;
        upgradePerCountMutiplier = upgradeSO.upgradePerCountMutiplier;
        upgradePerTime = upgradeSO.upgradePerTime;
        unlocked = false;


        //檢查是否有沒有購買過(適用於有存檔時)
        if (currentCount > 0)
        {
            unlocked = true;
            itemImg.color = Color.white;
            itemNameText.text = upgradeSO.itemName;
        }
        else
        {
            itemImg.color = Color.black;
            itemNameText.text = "????";
        }

    }
}
