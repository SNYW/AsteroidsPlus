using System;
using System.Linq;
using GameData;
using UnityEngine;

public class Missile : Projectile
{
    private Asteroid _targetAsteroid;
    
    protected new void OnEnable()
    { 
        base.OnEnable();
        _targetAsteroid = null;
        GetTarget();
    }

    private void Update()
    {
        if (_targetAsteroid == null ||!_targetAsteroid.gameObject.activeSelf)
        {
            GetTarget();
        }
        Vector3 vectorToTarget = _targetAsteroid.transform.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: vectorToTarget);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 200 * Time.deltaTime);

        _rb.AddForce((_targetAsteroid.transform.position-transform.position).normalized*speed*100*Time.deltaTime, ForceMode2D.Force);
    }

    private void GetTarget()
    {
        if (ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Asteroids).GetActiveAmount() <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
        
        var allAsteroids =  ObjectPoolManager
            .GetPool(ObjectPool.ObjectPoolName.Asteroids)
            .GetAllActive()
            .OrderBy(ast => Vector2.Distance(transform.position, ast.transform.position)).ToArray();

        if (!allAsteroids[0].transform.IsVisibleToCamera(Camera.main, null))
        {
            gameObject.SetActive(false);
            return;
        }
        
        _targetAsteroid = allAsteroids[0].GetComponent<Asteroid>();
    }
}
