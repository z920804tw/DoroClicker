using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("物件參考")]
    public GameObject clickTextPrefab;
    public GameObject clickSound;
    [SerializeField] Transform parent;
    public Upgrade[] upgrades;
    [Header("UI參考")]
    public TMP_Text coinCountText;
    public TMP_Text incomeText;

    [Header("變數設定")]

    public float oneClickCount = 1;
    public float idlePerSecondCount = 0; //每秒自動數量增加
    float coinCount = 0;
    public float CoinCount { get { return coinCount; } set { coinCount = value; } }
    float timer;

    //每秒點擊設定
    float clickCount;
    [SerializeField] float clickPerSecondCount = 0;//每秒點擊增加
    float coldDown;

    Coroutine reset;
    bool clicked;
    void Awake()
    {
    
    }
    // Start is called before the first frame update
    void Start()
    {


        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            coldDown += Time.deltaTime;
            if (coldDown >= 1)
            {
                coldDown = 0;
                clickPerSecondCount = clickCount;
                clickCount = 0;
            }
        }

        IdlePerSecondIncome();
        UpdateUI();
    }

    void IdlePerSecondIncome() //每秒自動增加
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
        //撥點擊聲音
        clickSound.GetComponent<AudioSource>().PlayOneShot(clickSound.GetComponent<AudioSource>().clip);

        //計算每秒點及次數
        clickCount++;
        clicked = true;

        if (reset != null)
        {
            StopCoroutine(reset);
        }
        reset = StartCoroutine(ResetClick());

        //產生點擊生產數值
        GameObject clickText = Instantiate(clickTextPrefab, Input.mousePosition, Quaternion.identity);
        clickText.transform.SetParent(parent);
        clickText.GetComponentInChildren<TMP_Text>().text = $"{NumberConvert(oneClickCount)}";

        UpdateUI();
    }
    public void UpdateUI()  //更新上方UI文字
    {
        coinCountText.text = $"{NumberConvert(coinCount)}";
        incomeText.text = $"{NumberConvert(idlePerSecondCount + (clickPerSecondCount * oneClickCount))}/s";
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


    IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(1f);
        clicked = false;
        clickCount = 0;
        clickPerSecondCount = 0;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
