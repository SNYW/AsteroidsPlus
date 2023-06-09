using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{

   [SerializeField] private RangeFloat minMaxRadius;
   [SerializeField] private RangeFloat minMaxPoints;
   [SerializeField] private RangeFloat minMaxPosOffset;
   [SerializeField] private RangeFloat minMaxDistanceOffset;
   [SerializeField] private RangeFloat minMaxInitSpeed;
   [SerializeField] private RangeFloat minMaxInitTorque;

   private LineRenderer _lr;
   private PolygonCollider2D _pc;
   private Rigidbody2D _rb;
   public float radius; 
   public float minSplitRadius;

   private void Awake()
   {
      _lr = GetComponentInChildren<LineRenderer>();
      _pc = GetComponent<PolygonCollider2D>();
      _rb = GetComponent<Rigidbody2D>();
   }

   private void OnEnable()
   {
      Initialize();
   }
   
   private void Initialize()
   {
      GenerateAsteroid();
      InitCollider();
      InitForces(Vector2.zero);
   }

   private void GenerateAsteroid(bool useCurrentRadius = false)
   {
      _rb.velocity = Vector2.zero;
      
      if(!useCurrentRadius) radius = minMaxRadius.RandomValue();
      var pointAmount = minMaxPoints.RandomValue();

      _lr.positionCount = (int)pointAmount;

      var angleOffset = 2 * Mathf.PI / _lr.positionCount;
      var angle = 0f;

      for (int i = 0; i < _lr.positionCount; i++)
      {

         Vector2 pointPos = new Vector2(
            Mathf.Cos(angle)*radius + minMaxPosOffset.RandomValue(), 
            Mathf.Sin(angle)*radius + minMaxPosOffset.RandomValue()
         );

         var distanceOffset = ((Vector2)transform.position - pointPos).normalized * minMaxDistanceOffset.RandomValue();

         _lr.SetPosition(i, pointPos+distanceOffset);
         
         angle += angleOffset;
      }

      _rb.mass = radius / 10;
   }

   private void InitCollider()
   {
      Vector3[] positions = new Vector3[_lr.positionCount];
      _lr.GetPositions(positions);
      
      _pc.SetPath(0, positions.Select(v => new Vector2(v.x, v.y)).ToArray());
   }

   private void InitForces(Vector2 target)
   {
      var randomOffset = new Vector2(Random.Range(-100, 100), Random.Range(-100, 100));
      var dirToCentre =target+randomOffset - (Vector2)transform.position;

      _rb.AddForce(dirToCentre * (_rb.mass * minMaxInitSpeed.RandomValue()), ForceMode2D.Force);
      var balancedRange = new RangeFloat(
         Random.Range(minMaxInitTorque.min, minMaxInitTorque.min / 2),
         Random.Range(minMaxInitTorque.max, minMaxInitTorque.max / 2));
      _rb.AddTorque(balancedRange.RandomValue()*_rb.mass, ForceMode2D.Impulse);
   }

   public void Hit()
   {
      SystemEventManager.RaiseEvent(SystemEventManager.ActionType.AsteroidDeath, this);
   }

   public void InitAsChild(float radiusOverride)
   {
      radius = radiusOverride;
      GenerateAsteroid(true);
      InitCollider();
      InitForces(transform.position);
   }
}
