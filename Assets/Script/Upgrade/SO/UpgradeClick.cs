using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Upgrade/UpgradeClcik")]
public class UpgradeClick : UpgradeSO
{
    public override void BuyUpgrade(Upgrade upgrade)
    {
        //升級Item項目
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        gameManager.oneClickCount = upgrade.CaculateIncomePerSecond();//增加升級後的值

    }
}
