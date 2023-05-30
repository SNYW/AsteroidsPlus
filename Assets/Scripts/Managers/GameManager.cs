using System;
using System.Collections;
using GameData;
using UnityEditor;
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
   [SerializeField] private float expPerLevel;
   [SerializeField] private int maxAsteroidsPerLevel;
   [SerializeField] private float asteroidSpawnDelay;
   [SerializeField] private float asteroidSpawnY;
   [SerializeField] private float asteroidSpawnX;
   [SerializeField] private GameState gameState;

   private int _currentAsteroids;
   [SerializeField] private int _currentLevel;
   private int _currentExp;
   private bool _isMaxLevel;
   
   private void Awake()
   {
      _currentAsteroids = 0;
      _currentLevel = 1;
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
            ManageLevel();
            break;
         case SystemEventManager.ActionType.AsteroidSpawn when payload is Asteroid:
            _currentAsteroids++;
            break;
      }
   }

   private void ManageLevel()
   {
      _currentExp++;
      SystemEventManager.RaiseEvent(SystemEventManager.ActionType.ExpGained, _currentExp/(expPerLevel*_currentLevel));
      if (_currentExp >= expPerLevel * _currentLevel)
      {
         GameLevelUp();
      }
   }

   private void GameLevelUp()
   {
      _currentExp = 0;
      _currentLevel = Mathf.Clamp(_currentLevel+1, 0, maxLevel);
      _isMaxLevel = _currentLevel == maxLevel;
      SystemEventManager.RaiseEvent(SystemEventManager.ActionType.LevelUp, _currentLevel);
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
         if(_currentAsteroids < maxAsteroidsPerLevel*_currentLevel)
         {
            AsteroidManager.SpawnNewAsteroid();
         }

         yield return new WaitForSeconds(asteroidSpawnDelay);
      }
   }
}
