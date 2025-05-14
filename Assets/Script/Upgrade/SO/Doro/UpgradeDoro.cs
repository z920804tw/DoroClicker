using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Upgrade/UpgradeDoro")]
public class UpgradeDoro : UpgradeSO
{
    public override void BuyUpgrade(Upgrade upgrade)
    {
        //升級doro項目
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();

        if (gameManager.idlePerSecondCount > 0)
        {
            gameManager.idlePerSecondCount -= upgrade.PrevIncome; //扣除原先的值    
        }

        gameManager.idlePerSecondCount += upgrade.CaculateIncomePerSecond();//增加升級後的值

    }

}
