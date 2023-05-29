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
   }

   private void Start()
   {
      gameState = GameState.Playing;
      StartCoroutine(SpawnAsteroid());
   }

   private IEnumerator SpawnAsteroid()
   {
      var alternateY = true;
      while(gameState == GameState.Playing)
      {
         var spawnPos = new Vector2(
            Random.Range(-asteroidSpawnX, asteroidSpawnX),
            alternateY ? asteroidSpawnY : -asteroidSpawnY
         );
         AsteroidManager.SpawnAsteroid(spawnPos);
         alternateY = !alternateY;
         yield return new WaitForSeconds(asteroidSpawnDelay);
      }
   }

   private Vector2 GetOffCameraSpawnPosition()
   {
      var mainCam = Camera.main;
      var viewBounds = new Bounds();
      return Vector2.zero;
   }
   
}
