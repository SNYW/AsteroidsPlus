using GameData;
using UnityEngine;

public static class AsteroidManager
{

    private static ObjectPool _asteroidPool = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Asteroids);
    private static bool _alternateY = true;
    private static float _asteroidSpawnY;
    private static float _asteroidSpawnX;

    public static void Init(float spawnX, float spawnY)
    {
        _asteroidSpawnX = spawnX;
        _asteroidSpawnY = spawnY;
    }

    
    public static void SpawnAsteroid(Vector3 position)
    {
        var asteroid = _asteroidPool.GetPooledObject();
        asteroid.transform.position = position;
        asteroid.SetActive(true);
    }

    public static void SpawnChildAsteroids(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            SpawnAsteroid(GetSafeSpawnPosition());
        }
    }

    public static void OnAsteroidDestroy(Asteroid asteroid)
    {
        asteroid.gameObject.SetActive(false);
    }
    
    public static Vector2 GetSafeSpawnPosition()
    {
        _alternateY = !_alternateY;
        
        return new Vector2(
            Random.Range(-_asteroidSpawnX, _asteroidSpawnX),
            _alternateY ? _asteroidSpawnY : -_asteroidSpawnY
        );
    }
}
