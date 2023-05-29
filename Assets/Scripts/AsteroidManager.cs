using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public static class AsteroidManager
{

    private static ObjectPool _asteroidPool = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Asteroids);

    
    public static void SpawnAsteroid(Vector3 position)
    {
        var asteroid = _asteroidPool.GetPooledObject();
        asteroid.transform.position = position;
        asteroid.SetActive(true);
    }

    public static void OnAsteroidDestroy(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
    }
}
