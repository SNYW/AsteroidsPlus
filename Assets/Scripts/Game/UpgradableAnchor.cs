using UnityEngine;

public class UpgradableAnchor : MonoBehaviour
{
    [SerializeField] private ShipUpgrade upgrade;

    private GameObject _upgradeGameObject;

    private void Start()
    {
        _upgradeGameObject = Instantiate(
            upgrade.GetPrefabForCurrentLevel(), 
            transform.position, 
            transform.rotation, 
            transform
        );
    }

    private void OnEnable()
    {
        SystemEventManager.Subscribe(OnGameAction);
    }

    private void OnDisable()
    { 
        SystemEventManager.Unsubscribe(OnGameAction);
    }

    private void OnGameAction(SystemEventManager.ActionType type, object payload)
    {
        switch (type)
        {
            case SystemEventManager.ActionType.ShipUpgraded when payload is ShipUpgrade targetUpgrade && targetUpgrade == upgrade:
                UpgradeShip();
                break;
            case SystemEventManager.ActionType.GameReset:
                ResetUpgrade();
                break;
        }
    }

    private void UpgradeShip()
    {
        Destroy(_upgradeGameObject);

        _upgradeGameObject = Instantiate(
            upgrade.GetPrefabForCurrentLevel(), 
            transform.position, 
            transform.rotation, 
            transform
        );
    }

    private void ResetUpgrade()
    {
        Destroy(_upgradeGameObject);

        _upgradeGameObject = Instantiate(
            upgrade.GetPrefabForLevel(0), 
            transform.position, 
            transform.rotation, 
            transform
        );
    }
}
