using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeDoro : MonoBehaviour
{
    [Header("物件參考")]
    public TMP_Text incomeText;
    public TMP_Text priceText;
    public TMP_Text itemNameText;
    public GameObject blockUI;
    Button current;
    [Header("數值設定")]

    public UpgradeSO upgradeSO;
    [SerializeField] Image itemImg;
    float startPrice;    //開始價錢
    float upgradePriceMutiplier;
    float upgradePerTime;
    int currentCount = 0;
    bool unlocked;
    GameManager gameManager;

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
            float prevIncome = CaculateIncomePerSecond();
            currentCount++;
            UpdateUI();

            gameManager.idlePerSecondCount -= prevIncome; //扣除原先的值    
            gameManager.idlePerSecondCount += CaculateIncomePerSecond();//增加升級後的值

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
        return currentCount * upgradePerTime;
    }



    void InitializationInfo()
    {
        itemNameText.text = upgradeSO.itemName;
        itemImg.sprite = upgradeSO.itemImg;
        startPrice = upgradeSO.startPrice;
        upgradePriceMutiplier = upgradeSO.upgradePriceMutiplier;
        upgradePerTime = upgradeSO.upgradePerTime;
        unlocked = false;


        //預設沒有購買狀態
        itemImg.color = Color.black;
        itemNameText.text = "????";
    }
}
