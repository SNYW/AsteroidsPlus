
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ShipUpgradeManager
{
   private static Dictionary<ShipUpgrade.UpgradeName, ShipUpgrade> _upgrades;
   private static int _upgradePoints;
   
   public static void Init()
   { 
      var allupgrades = Resources.LoadAll("Data/Upgrades", typeof(ShipUpgrade)).Cast<ShipUpgrade>();

      _upgrades = new Dictionary<ShipUpgrade.UpgradeName, ShipUpgrade>();
        
      foreach (var upgrade in allupgrades)
      {
         upgrade.Init();
         _upgrades.Add(upgrade.upgradeName, upgrade);
      }

      _upgradePoints = 0;
      SystemEventManager.Subscribe(OnGameAction);
   }

   private static void OnGameAction(SystemEventManager.ActionType type, object payload)
   {
      switch (type)
      {
         case SystemEventManager.ActionType.LevelUp:
            _upgradePoints++;
            break;
         case SystemEventManager.ActionType.ShipUpgraded:
            _upgradePoints--;
            break;
      }
   }

   public static void Upgrade(ShipUpgrade.UpgradeName upgradeName)
   {
      if (!_upgrades.TryGetValue(upgradeName, out var upgrade)) return;

      if (upgrade.isMaxed) return;
      
      upgrade.LevelUp();
      SystemEventManager.RaiseEvent(SystemEventManager.ActionType.ShipUpgraded, upgrade);
   }

   public static bool CanUpgrade()
   {
      return _upgradePoints > 0;
   }
}
