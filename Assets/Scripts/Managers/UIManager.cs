using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject UpgradePanel;

    private void OnEnable()
    {
        UpgradePanel.SetActive(false);
        SystemEventManager.Subscribe(OnGameAction);
    }

    private void OnGameAction(SystemEventManager.ActionType type, object payload)
    {
        switch (type)
        {
            case SystemEventManager.ActionType.ShipUpgraded 
                or SystemEventManager.ActionType.ExpGained
                or SystemEventManager.ActionType.LevelUp:
                UpgradePanel.SetActive(ShipUpgradeManager.CanUpgradeAny());
                break;
            case SystemEventManager.ActionType.GameReset:
                UpgradePanel.SetActive(false);
                break;
        }
    }
}
