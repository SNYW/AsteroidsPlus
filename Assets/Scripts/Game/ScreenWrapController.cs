using System;
using System.Collections;
using UnityEngine;

public class ScreenWrapController : MonoBehaviour
{
    [SerializeField] private bool requireVisible;
    [SerializeField] private bool canCollideOffscreen;
    [SerializeField] private float wrapDelay;
    
    private LineRenderer _lr;
    private bool _hasBeenVisible;
    private bool _canWrap = true;
    private Camera _mainCam;
    private Collider2D[] _colliders;

    private void OnEnable()
    { 
        _hasBeenVisible = false;
        _mainCam = Camera.main;
        _colliders = GetComponentsInChildren<Collider2D>();
        _lr = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        if (_lr.isVisible)
            _hasBeenVisible = true;
        
        if(!canCollideOffscreen)
            foreach (var collider2D in _colliders)
            {
                collider2D.enabled = _lr.isVisible;
            }

        if (requireVisible && !_hasBeenVisible) return;
        
        if (_canWrap && !transform.IsVisibleToCamera(_mainCam,_lr))
        {
            StartCoroutine(Wrap());
        }
    }
    
    private IEnumerator Wrap()
    {
        _canWrap = false; 
        transform.UpdateScreenWrap(); 
        yield return new WaitForSeconds(wrapDelay); 
        _canWrap = true;
    }
}
