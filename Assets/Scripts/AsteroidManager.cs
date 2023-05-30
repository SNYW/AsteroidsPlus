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

    
    public static void SpawnAsteroid(Vector3 position, bool overrideRadius = false, float radius = 0)
    {
        var asteroid = _asteroidPool.GetPooledObject().GetComponent<Asteroid>();
        asteroid.transform.position = position;
        asteroid.gameObject.SetActive(true);
        if(overrideRadius) asteroid.InitAsChild(radius);
    }

    public static void SpawnChildAsteroids(int amount, Vector2 position)
    {
        if(amount <= 1) return;
        for (int i = 0; i < amount; i++)
        {
            var randomOffset = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
            SpawnAsteroid(position+randomOffset, true, 20);
        }
    }

    public static void OnAsteroidDestroy(Asteroid asteroid)
    {
        var hitParticles = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.AsteroidHitParticles).GetPooledObject()
            .GetComponent<ParticleSystem>();
        
        hitParticles.transform.position = asteroid.transform.position;
        hitParticles.gameObject.SetActive(true);
        hitParticles.Play();
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
