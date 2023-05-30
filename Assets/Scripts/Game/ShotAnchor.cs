using GameData;
using UnityEngine;

public class ShotAnchor : MonoBehaviour
{
    [SerializeField] private ProjectileController.ShotType shotType;
    [SerializeField] private ObjectPool.ObjectPoolName projectilePool;
    
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
        if (type is not SystemEventManager.ActionType.ShotFired) return;
        if (payload is not ProjectileController.ShotType shottype || shottype != shotType) return;
        
        var projectile = ObjectPoolManager.GetPool(projectilePool).GetPooledObject(); 
        projectile.transform.position = transform.position;
        projectile.transform.up = transform.up;
        projectile.SetActive(true);
    }
}
