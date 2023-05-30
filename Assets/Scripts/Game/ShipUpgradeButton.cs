using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeButton : MonoBehaviour
{
   [SerializeField] public ShipUpgrade.UpgradeName upgradeName;

   private Button _button;

   private void Awake()
   {
      _button = GetComponent<Button>();
   }

   private void Update()
   {
      _button.interactable = ShipUpgradeManager.CanUpgrade(upgradeName);
   }

   public void OnClicked()
   {
      ShipUpgradeManager.Upgrade(upgradeName);
   }
}
