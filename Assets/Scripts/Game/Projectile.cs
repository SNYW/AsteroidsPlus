using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime;
    
    protected Rigidbody2D _rb;

    protected void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    protected void OnEnable()
    {
        StartCoroutine(Disable());
    }

    protected IEnumerator Disable()
    {
        yield return new WaitForSeconds(lifetime);
        gameObject.SetActive(false);
    }
    protected void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent<Asteroid>(out var asteroid))
        {
            asteroid.Hit();
        }
        
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
