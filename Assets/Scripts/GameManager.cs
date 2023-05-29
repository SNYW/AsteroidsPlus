using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   enum GameState
   {
      Playing,
      Paused,
      Ended
   }
   
   [SerializeField] private float asteroidSpawnDelay;
   [SerializeField] private float asteroidSpawnY;
   [SerializeField] private float asteroidSpawnX;
   [SerializeField] private GameState gameState;
   
   private void Awake()
   {
      ObjectPoolManager.InitPools();
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
         AsteroidManager.SpawnAsteroid(AsteroidManager.GetSafeSpawnPosition());
         yield return new WaitForSeconds(asteroidSpawnDelay);
      }
   }
}
