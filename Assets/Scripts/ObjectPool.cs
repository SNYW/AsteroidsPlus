using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameData
{
    [CreateAssetMenu(fileName = "new Pool", menuName = "Game Data/ Object Pool")]
    public class ObjectPool : ScriptableObject
    {
        public ObjectPoolName poolName;
        [SerializeField] private GameObject pooledObject;
        [SerializeField] private int minAmount;

        private List<GameObject> _pool;

        public GameObject GetPooledObject()
        {
            if (_pool.Any(go => !go.activeInHierarchy))
            {
                return _pool.First(go => !go.activeInHierarchy);
            }

            var newPooledObject = Instantiate(pooledObject, Vector2.zero, Quaternion.identity);
            _pool.Add(newPooledObject);
            return newPooledObject;
        }

        public void InitPool()
        {
            if (_pool.Any())
            {
                _pool.ForEach(Destroy);
                _pool.Clear();
            }
            
            for (int i = 0; i < minAmount; i++)
            {
                var newPooledObject = Instantiate(pooledObject, Vector2.zero, Quaternion.identity);
                newPooledObject.SetActive(false);
                _pool.Add(newPooledObject);     
            }
        }

        public enum ObjectPoolName
        {
            Asteroids,
            Bullet,
            Missile
        }
    }
}
