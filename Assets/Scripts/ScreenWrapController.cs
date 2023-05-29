using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrapController : MonoBehaviour
{
    [SerializeField] private bool requireVisible;
    private LineRenderer _lr;
    private bool _hasBeenVisible;
    private bool _canWrap = true;

    private void Awake()
    {
        _hasBeenVisible = false;
        _lr = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        if (_lr.isVisible)
            _hasBeenVisible = true;
        
        if (requireVisible && !_hasBeenVisible) return;
        
        if (_canWrap && !transform.IsVisibleToCamera(Camera.main, _lr))
        {
            StartCoroutine(Wrap());
        }
    }
    
    private IEnumerator Wrap()
    {
        _canWrap = false; 
        transform.UpdateScreenWrap(); 
        yield return new WaitForSeconds(0.2f); 
        _canWrap = true;
    }
}
