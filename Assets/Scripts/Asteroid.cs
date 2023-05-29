using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

   [SerializeField] private RangeFloat minMaxRadius;
   [SerializeField] private RangeFloat minMaxPointOffset;
   [SerializeField] private RangeFloat minMaxOffset;
   
   private LineRenderer _lr;
   private PolygonCollider2D _pc;

   private void Awake()
   {
      _lr = GetComponentInChildren<LineRenderer>();
      _pc = GetComponent<PolygonCollider2D>();
      GenerateAsteroid();
   }

   private void GenerateAsteroid()
   {
      var radius = minMaxRadius.RandomValue();
      var pointAmount = radius + minMaxPointOffset.RandomValue();

      _lr.positionCount = (int)pointAmount;

      var angleOffset = 2 * Mathf.PI / pointAmount;
      var angle = 0f;

      for (int i = 0; i < pointAmount-1; i++)
      {
         
         Vector2 pointPos = new Vector2(
            Mathf.Cos(angle)*radius + minMaxOffset.RandomValue(), 
            Mathf.Sin(angle)*radius + minMaxOffset.RandomValue()
         );

         _lr.SetPosition(i, pointPos);
         
         angle += angleOffset;
      }

      InitCollider();
   }

   private void InitCollider()
   {
      Vector3[] positions = new Vector3[_lr.positionCount];
      _lr.GetPositions(positions);
      
      _pc.SetPath(0, positions.Select(v => new Vector2(v.x, v.y)).ToArray());
   }
}
