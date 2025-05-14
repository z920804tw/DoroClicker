using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UpgradeSO : ScriptableObject
{
    public Sprite itemImg;
    public string itemName;
    public int startPrice;
    public float upgradePriceMutiplier;
    public float upgradePerCountMutiplier;
    public float upgradePerTime;
    public abstract void BuyUpgrade(Upgrade upgrade);
}
