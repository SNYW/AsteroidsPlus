using UnityEngine;

public class ShipUpgradeButton : MonoBehaviour
{
   [SerializeField] public ShipUpgrade.UpgradeName upgradeName;
   
   public void OnClicked()
   {
      ShipUpgradeManager.Upgrade(upgradeName);
   }
}
