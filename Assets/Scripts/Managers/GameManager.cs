using System;
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

   [SerializeField] private int maxLevel;
   [SerializeField] private float pointsPerLevel;
   [SerializeField] private int maxAsteroidsPerLevel;
   [SerializeField] private float asteroidSpawnDelay;
   [SerializeField] private float asteroidSpawnY;
   [SerializeField] private float asteroidSpawnX;
   [SerializeField] private GameState gameState;

   public int _currentAsteroids;
   
   private void Awake()
   {
      _currentAsteroids = 0;
      ShipUpgradeManager.Init();
      ObjectPoolManager.InitPools();
      AsteroidManager.Init(asteroidSpawnX, asteroidSpawnY);
      SystemEventManager.Subscribe(OnGameAction);
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.G))
      {
         ShipUpgradeManager.Upgrade(ShipUpgrade.UpgradeName.Shield);
      }
   }

   private void OnGameAction(SystemEventManager.ActionType type, object payload)
   {
      switch (type)
      {
         case SystemEventManager.ActionType.AsteroidDeath when payload is Asteroid:
            _currentAsteroids--;
            break;
         case SystemEventManager.ActionType.AsteroidSpawn when payload is Asteroid:
            _currentAsteroids++;
            break;
      }
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
         if(_currentAsteroids < maxAsteroids)
         {
            AsteroidManager.SpawnNewAsteroid();
         }

         yield return new WaitForSeconds(asteroidSpawnDelay);
      }
   }
}
