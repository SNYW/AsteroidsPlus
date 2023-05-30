using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Upgrade", menuName = "Game Data/Ship Upgrade")]
public class ShipUpgrade : ScriptableObject
{
   public string upgradeName; 
   [SerializeField] private int startLevel = 0;
   [SerializeField] private string anchorName;
   [SerializeField] private List<GameObject> upgradePrefabs;
  
   private int _currentLevel;

   public void LevelUp()
   {
      _currentLevel++;
   }

   public GameObject GetPrefabForCurrentLevel()
   {
      return upgradePrefabs[_currentLevel];
   }
}
