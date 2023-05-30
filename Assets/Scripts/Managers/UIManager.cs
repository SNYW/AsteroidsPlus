using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject UpgradePanel;

    private void OnEnable()
    {
        SystemEventManager.Subscribe(OnGameAction);
        UpgradePanel.SetActive(false);
    }

    private void OnGameAction(SystemEventManager.ActionType type, object payload)
    {
        switch (type)
        {
            case SystemEventManager.ActionType.LevelUp:
                UpgradePanel.SetActive(true);
                break;
            case SystemEventManager.ActionType.ShipUpgraded:
                UpgradePanel.SetActive(ShipUpgradeManager.CanUpgrade());
                break;
        }
    }
}
