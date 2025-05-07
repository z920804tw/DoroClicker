using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Upgrade/UpgradeInfo")]
public class UpgradeSO : ScriptableObject
{
    public Sprite itemImg;
    public string itemName;
    public int startPrice;
    public float upgradePriceMutiplier;
    public float upgradePerTime;
}
