using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float baseShotCooldown;
    [SerializeField] private float baseMissileCooldown;

    private bool _canShoot;

    public enum ShotType
    {
        Bullet,
        Missile
    }

    private void Awake()
    {
        _canShoot = true;
    }

    private void OnEnable()
    {
        StartCoroutine(FireMissiles());
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator FireMissiles()
    {
        while (true)
        {
            yield return new WaitForSeconds(baseMissileCooldown);
            SystemEventManager.RaiseEvent(SystemEventManager.ActionType.ShotFired, ShotType.Missile);
        }
    }

    private IEnumerator Shoot()
    {
        _canShoot = false;
        SystemEventManager.RaiseEvent(SystemEventManager.ActionType.ShotFired, ShotType.Bullet);
        yield return new WaitForSeconds(baseShotCooldown);
        _canShoot = true;
    }
}
