using System.Collections;
using GameData;
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
   [SerializeField] private UFO UFO;
   [SerializeField] private float UFOSpawnTime;
   [SerializeField] private float UFOX;
   [SerializeField] private RangeFloat UFOMinMaxY;
   [SerializeField] private GameState gameState;
   [SerializeField] private TMP_Text scoreText;
   [SerializeField] private TMP_Text highScoreText;

   private int _currentLevel;
   private int _currentExp;
   private bool _isMaxLevel;
   private int score;

   private void Awake()
   {
      highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore")}";
      scoreText.text = "Score: 0";
      
      _currentLevel = 1;
      ShipUpgradeManager.Init();
      ObjectPoolManager.InitPools();
      AsteroidManager.Init(asteroidSpawnX, asteroidSpawnY, false);
      SystemEventManager.Subscribe(OnGameAction);
   }

   private void OnGameAction(SystemEventManager.ActionType type, object payload)
   {
      switch (type)
      {
         case SystemEventManager.ActionType.AsteroidDeath when payload is Asteroid:
            ManageLevel();
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
      StartCoroutine(SpawnUFO());
   }

   private IEnumerator SpawnUFO()
   {
      while(gameState == GameState.Playing)
      {
         yield return new WaitForSeconds(UFOSpawnTime);
         if (UFO.gameObject.activeSelf) continue;
         
         var spawnx = Random.Range(0, 1) >= 0.5f ? UFOX : -UFOX;
         var spawny = UFOMinMaxY.RandomValue();
         var spawnPos = new Vector2(spawnx, spawny);
         UFO.transform.position = spawnPos;
         UFO.gameObject.SetActive(true);
      }
   }

   private IEnumerator SpawnAsteroid()
   {
      while(gameState == GameState.Playing)
      {
         var currentAsteroids = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.Asteroids).GetActiveAmount();
         if(currentAsteroids <= maxAsteroidsPerLevel)
         {
            AsteroidManager.SpawnNewAsteroid();
         }

         yield return new WaitForSeconds(asteroidSpawnDelay);
      }
   }

   private void OnPlayerDeath()
   {
      ResetGame();
   }

   private void ResetGame()
   {
      StopAllCoroutines();
      UFO.gameObject.SetActive(false);
      
      if (PlayerPrefs.GetInt("HighScore") < score)
      {
         PlayerPrefs.SetInt("HighScore", score);
      }
      highScoreText.text = $"High Score: {PlayerPrefs.GetInt("HighScore")}";
      
      _currentLevel = 1;
      _currentExp = 0;
      score = 0;
      scoreText.text = "Score: 0";
      
      ShipUpgradeManager.Init();
      ObjectPoolManager.InitPools();
      AsteroidManager.Init(asteroidSpawnX, asteroidSpawnY, true);
      StartCoroutine(SpawnUFO());
      StartCoroutine(SpawnAsteroid());
   }
   
}
