using System.Collections;
using GameData;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   enum GameState
   {
      Playing,
      Paused,
      Ended
   }

   [SerializeField] private int maxAsteroids;
   [SerializeField] private float asteroidSpawnDelay;
   [SerializeField] private float asteroidSpawnY;
   [SerializeField] private float asteroidSpawnX;
   [SerializeField] private GameState gameState;

   private ObjectPool _asteroidPool;
   private void Awake()
   {
      ObjectPoolManager.InitPools();
      _asteroidPool = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Asteroids);
      AsteroidManager.Init(asteroidSpawnX, asteroidSpawnY);
   }

   private void Start()
   {
      gameState = GameState.Playing;
      StartCoroutine(SpawnAsteroid());
   }

   private IEnumerator SpawnAsteroid()
   {
      while(gameState == GameState.Playing)
      {
         if(_asteroidPool.GetActiveAmount() < maxAsteroids)
            AsteroidManager.SpawnAsteroid(AsteroidManager.GetSafeSpawnPosition());
         
         yield return new WaitForSeconds(asteroidSpawnDelay);
      }
   }
}
