
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ShipUpgradeManager
{
   private static Dictionary<ShipUpgrade.UpgradeName, ShipUpgrade> _upgrades;
   public static void Init()
   { 
      var allupgrades = Resources.LoadAll("Data/Upgrades", typeof(ShipUpgrade)).Cast<ShipUpgrade>();

      _upgrades = new Dictionary<ShipUpgrade.UpgradeName, ShipUpgrade>();
        
      foreach (var upgrade in allupgrades)
      {
         upgrade.Init();
         _upgrades.Add(upgrade.upgradeName, upgrade);
      }
   }

   public static void Upgrade(ShipUpgrade.UpgradeName upgradeName)
   {
      if (!_upgrades.TryGetValue(upgradeName, out var upgrade)) return;
      
      upgrade.LevelUp();
      SystemEventManager.RaiseEvent(SystemEventManager.ActionType.ShipUpgraded, upgrade);
   }
}
