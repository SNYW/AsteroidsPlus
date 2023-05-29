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
   [SerializeField] private GameState gameState;
   [SerializeField] private RangeFloat minAsteroidX;
   
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
      while(gameState == GameState.Playing)
      { 
         AsteroidManager.SpawnAsteroid(Vector3.zero);
         
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
