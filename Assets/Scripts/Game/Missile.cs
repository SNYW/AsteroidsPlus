using System.Linq;
using GameData;
using UnityEngine;

public class Missile : Projectile
{
    private Asteroid _targetAsteroid;
    
    protected new void OnEnable()
    { 
        base.OnEnable(); 
        GetTarget();
    }

    private void Update()
    {
        if (_targetAsteroid == null || !_targetAsteroid.isActiveAndEnabled)
        {
            GetTarget();
        }
        
        transform.LookAt(_targetAsteroid.transform);
        _rb.AddForce((transform.position - _targetAsteroid.transform.position).normalized*speed, ForceMode2D.Force);
    }

    private void GetTarget()
    {
        if (ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Asteroids).GetActiveAmount() <= 0) gameObject.SetActive(false);
        
        var allAsteroids =  ObjectPoolManager
            .GetPool(ObjectPool.ObjectPoolName.Asteroids)
            .GetAllActive()
            .OrderBy(ast => Vector2.Distance(transform.position, ast.transform.position)).ToArray();
        
        _targetAsteroid = allAsteroids[0].GetComponent<Asteroid>();
    }
}
