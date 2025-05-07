using System;
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
    public float idlePerSecondCount = 0; //每秒自動數量增加
    [SerializeField] float clickPerSecondCount = 0;//每秒點擊增加
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
        UpdateUI();
    }

    void IdlePerSecondIncome()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            coinCount += idlePerSecondCount;
        }
    }

    public void ClickAction() //點擊中央按鈕時執行
    {
        coinCount += oneClickCount;
        clickPerSecondCount = oneClickCount;
        // clicked = true;
        UpdateUI();
    }
    public void UpdateUI()  //更新上方UI文字
    {
        coinCountText.text = $"{NumberConvert(coinCount)}";
        incomeText.text = $"{NumberConvert(idlePerSecondCount)}/s";
    }

    public bool CanBuy(int price) //是否能購買的功能
    {
        if (coinCount >= price)
        {
            coinCount -= price;
            return true;
        }
        else return false;
    }


    public string NumberConvert(float number)
    {
        string[] unit = { "", "K", "M", "B", "T" };
        int index = 0;
        while (number >= 1000 && index < unit.Length - 1) //有超過該單位的容量上限時，就會轉換 (每個單位最高為999)
        {
            number = number / 1000;
            index++;

            //例如 number=2900 更新後 number=2.9 ，index=1   
        }
        if (index == 0)
        {
            return $"{number}{unit[index]}";
        }
        else
        {
            return $"{number:0.00}{unit[index]}";
        }
    }
}
