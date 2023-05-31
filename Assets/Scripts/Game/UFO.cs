using System;
using System.Collections;
using GameData;
using UnityEngine;

public class UFO : MonoBehaviour
{
   [SerializeField] float moveSpeed;
   [SerializeField] private float shotCooldown;
   
   private GameObject _playerShip;
   private Vector2 moveDirection;

   private void OnEnable()
   {
      _playerShip = GameObject.Find("Player Ship");
      moveDirection = transform.position.x > 0 ? Vector2.left : Vector2.right;
      StartCoroutine(ShootAtShip());
   }

   IEnumerator ShootAtShip()
   {
      while (gameObject.activeSelf)
      {
         yield return new WaitForSeconds(shotCooldown);
         
         var shot = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.UFOShots).GetPooledObject();

         var dirToShip = (_playerShip.transform.position - transform.position).normalized;
         shot.transform.position = transform.position;
         shot.transform.up = dirToShip;
         shot.SetActive(true);
      }
   }

   private void Update()
   {
      transform.Translate(moveDirection*moveSpeed*100*Time.deltaTime, Space.World);
   }

   private void OnCollisionEnter2D(Collision2D col)
   {
      if(!col.gameObject.TryGetComponent(typeof(Asteroid), out var a))
      {
         var particles = ObjectPoolManager.GetPool(ObjectPool.ObjectPoolName.AsteroidHitParticles).GetPooledObject();
         particles.transform.position = transform.position;
         gameObject.SetActive(false);
      }
   }
}
