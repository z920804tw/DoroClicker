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
    public GameObject blockUI;
    Button current;
    [Header("數值設定")]
    [SerializeField] int startPrice = 10;
    public float upgradePriceMutiplier;
    public float upgradePerTime = 0.2f;
    GameManager gameManager;
    int currentCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        current = GetComponent<Button>();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        // if (gameManager.CanBuy(CaculatePrice())) //可以購買的話
        // {
        //     current.interactable = true;
        //     blockUI.SetActive(false);
        // }
        // else
        // {
        //     current.interactable = false;
        //     blockUI.SetActive(true);
        // }
    }


    public void BuyUpgrade()
    {
        bool canBuy = gameManager.CanBuy(CaculatePrice());

        if (canBuy)
        {
            float prevIncome= CaculateIncomePerSecond();

            currentCount++;
            UpdateUI();

            gameManager.idlePerSecondCount-=prevIncome;
            gameManager.idlePerSecondCount+=CaculateIncomePerSecond();
            gameManager.UpdateUI();
        }

    }

    public void UpdateUI()
    {
        priceText.text = $"{CaculatePrice()}";
        incomeText.text = $"x{currentCount} {CaculateIncomePerSecond()}/s";
    }
    int CaculatePrice()
    {
        int price = Mathf.RoundToInt(startPrice * Mathf.Pow(upgradePriceMutiplier, currentCount));
        return price;
    }

    float CaculateIncomePerSecond()
    {
        float income= currentCount* Mathf.Pow(1.2f,upgradePerTime);

        return currentCount * upgradePerTime;
    }
}
