using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] Transform bulletAnchor;
    [SerializeField] private List<Transform> missileAnchors;
    [SerializeField] private float baseShotCooldown;

    private bool _canShoot;

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
        var bullet = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Bullets).GetPooledObject();
        bullet.transform.position = bulletAnchor.transform.position;
        bullet.transform.up = bulletAnchor.transform.up;
        bullet.SetActive(true);
        
        yield return new WaitForSeconds(baseShotCooldown);
        
        _canShoot = true;
    }
}
