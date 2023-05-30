using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Upgrade", menuName = "Game Data/Ship Upgrade")]
public class ShipUpgrade : ScriptableObject
{
   public UpgradeName upgradeName; 
   [SerializeField] private int startLevel = 0;
   [SerializeField] private List<GameObject> upgradePrefabs;
  
   private int _currentLevel;
   public bool isMaxed;

   public void Init()
   {
      isMaxed = false;
      _currentLevel = startLevel;
   }

   public void LevelUp()
   {
      _currentLevel++;
      isMaxed = _currentLevel == upgradePrefabs.Count - 1;
   }

   public GameObject GetPrefabForCurrentLevel()
   {
      return upgradePrefabs[_currentLevel];
   }
   
   public GameObject GetPrefabForLevel(int level)
   {
      return upgradePrefabs[level];
   }
   
   public enum UpgradeName
   {
      Gun,
      Shield,
      Missile
   }
}
