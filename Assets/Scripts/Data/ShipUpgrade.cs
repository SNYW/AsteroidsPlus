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

   public void Init()
   {
      _currentLevel = startLevel;
   }

   public void LevelUp()
   {
      _currentLevel++;
   }

   public GameObject GetPrefabForCurrentLevel()
   {
      return upgradePrefabs[_currentLevel];
   }
   
   public enum UpgradeName
   {
      Gun
   }
}
