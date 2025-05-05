using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject mainObject;
    [Header("UI參考")]
    public TMP_Text coinCountText;
    public TMP_Text incomeText;

    [Header("變數設定")]

    public float oneClickCount = 1;
    public float idlePerSecondCount=0;
    float coinCount = 0;
    public float CoinCount { get { return coinCount; } set { coinCount = value; } }

    float timer;



    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        IdlePerSecondIncome();
    }

    public void IdlePerSecondIncome()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer=0;
            coinCount+=idlePerSecondCount;
            UpdateUI();
        }
    }

    public void ClickAction()
    {
        coinCount += oneClickCount;
        UpdateUI();
    }
    public void UpdateUI()
    {
        coinCountText.text = $"{coinCount}";
        incomeText.text=$"{idlePerSecondCount}/s";
    }

    public bool CanBuy(int price)
    {
        if (coinCount >= price)
        {
            coinCount -= price;
            UpdateUI();
            return true;
        }
        else return false;
    }
}
