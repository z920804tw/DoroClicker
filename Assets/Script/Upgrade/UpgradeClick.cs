using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UpgradeClick : MonoBehaviour
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
    int startPrice;
    float upgradePriceMutiplier;
    float upgradePerTime;
    GameManager gameManager;
    int currentCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        current = GetComponent<Button>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        itemNameText.text = upgradeSO.itemName;
        itemImg.sprite = upgradeSO.itemImg;
        startPrice = upgradeSO.startPrice;
        upgradePriceMutiplier = upgradeSO.upgradePriceMutiplier;
        upgradePerTime = upgradeSO.upgradePerTime;

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
            currentCount++;
            UpdateUI();
            gameManager.oneClickCount = CaculateIncomePerSecond();//增加升級後的值
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

    float CaculateIncomePerSecond() //計算每秒產出
    {
        return currentCount * upgradePerTime;
    }
}
