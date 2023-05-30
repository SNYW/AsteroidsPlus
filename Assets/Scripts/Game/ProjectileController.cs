using System.Collections;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float baseShotCooldown;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canShoot)
        {
            StartCoroutine(Shoot());
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
