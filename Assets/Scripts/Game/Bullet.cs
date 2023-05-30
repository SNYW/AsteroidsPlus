using UnityEngine;

public class Bullet : Projectile
{
    private void Update()
    {
        _rb.velocity = transform.up * speed;
    }
}
