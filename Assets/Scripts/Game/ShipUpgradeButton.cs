using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipUpgradeButton : MonoBehaviour
{
   [SerializeField] private ShipUpgrade.UpgradeName upgradeName;
   [SerializeField] private KeyCode hotkey;

   private Button _button;

   private void Awake()
   {
      _button = GetComponent<Button>();
   }

   private void Update()
   {
      _button.interactable = ShipUpgradeManager.CanUpgrade(upgradeName);
      if (Input.GetKeyDown(hotkey) && _button.interactable)
      {
         OnClicked();
      }
   }

   public void OnClicked()
   {
      ShipUpgradeManager.Upgrade(upgradeName);
   }
}
