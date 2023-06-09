using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shieldSprite;
    [SerializeField] private float shieldRegenTime;
    
    private Collider2D _collider;
    private bool _shieldActive = true;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.density = 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(_shieldActive)
            StartCoroutine(ManageShield());
    }

    private IEnumerator ManageShield()
    {
        _shieldActive = false;
        _collider.enabled = false;
        shieldSprite.enabled = false;
        
        yield return new WaitForSeconds(shieldRegenTime);

        shieldSprite.enabled = true;
        _shieldActive = true;
        _collider.enabled = true;
    }
}
