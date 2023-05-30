using UnityEngine;

public class UpgradableAnchor : MonoBehaviour
{
    [SerializeField] private ShipUpgrade upgrade;

    private GameObject _upgradeGameObject;

    private void Awake()
    {
       _upgradeGameObject = transform.GetChild(0).gameObject;
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
        if (type is SystemEventManager.ActionType.ShipUpgraded && 
            payload is ShipUpgrade targetUpgrade && 
            targetUpgrade == upgrade)
        {
            UpgradeShip(upgrade);
        }
    }

    private void UpgradeShip(ShipUpgrade targetUpgrade)
    {
        Destroy(_upgradeGameObject);

        _upgradeGameObject = Instantiate(
            targetUpgrade.GetPrefabForCurrentLevel(), 
            transform.position, 
            Quaternion.identity, 
            transform
        );
    }
}
