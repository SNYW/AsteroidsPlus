using System.Collections;
using TMPro;
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
   [SerializeField] private TMP_Text scoreText;
   [SerializeField] private TMP_Text highScoreText;

   private int _currentAsteroids;
   private int _currentLevel;
   private int _currentExp;
   private bool _isMaxLevel;
   private int score;

   private void Awake()
   {
      _currentAsteroids = 0;
      _currentLevel = 1;
      ShipUpgradeManager.Init();
      ObjectPoolManager.InitPools();
      AsteroidManager.Init(asteroidSpawnX, asteroidSpawnY);
      SystemEventManager.Subscribe(OnGameAction);
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
         case SystemEventManager.ActionType.GameReset:
            OnPlayerDeath();
            break;
      }
   }

   private void ManageLevel()
   {
      score++;
      _currentExp++;
      SystemEventManager.RaiseEvent(SystemEventManager.ActionType.ExpGained, _currentExp/(expPerLevel*_currentLevel));
      if (_currentExp >= expPerLevel * _currentLevel)
      {
         GameLevelUp();
      }

      scoreText.text = $"Score: {score}";
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

   public void OnPlayerDeath()
   {
      ResetGame();
   }

   private void ResetGame()
   {
      if (PlayerPrefs.GetInt("HighScore") < score)
      {
         PlayerPrefs.SetInt("HighScore", score);
      }
      highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore")}";
      
      _currentAsteroids = 0;
      _currentLevel = 1;
      _currentExp = 0;
      score = 0;
      
      ShipUpgradeManager.Init();
      ObjectPoolManager.InitPools();
      AsteroidManager.Init(asteroidSpawnX, asteroidSpawnY);
   }
   
}
