using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Upgrade", menuName = "Game Data/Ship Upgrade")]
public class ShipUpgrade : ScriptableObject
{
   public string upgradeName; 
   [SerializeField] private int startLevel;
   [SerializeField] private string anchorName;
   [SerializeField] private List<GameObject> upgradePrefabs;
  
   private int currentLevel;

}
