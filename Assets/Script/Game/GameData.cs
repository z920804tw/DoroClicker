using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData
{
    public int[] upgradeCount;
    public float oneClickCount; //GM裡面的每次點及數量
    public float idlePerSecondCount; //GM中的每秒增加數量
    public float coinCount;         //金幣總數
}
