using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Vector2 _maxBounds;
    [SerializeField] private float _speed;
    [SerializeField] private float _turnRate;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ManageMovement();
    }

    private void ManageMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(transform.up * _speed, ForceMode2D.Force);
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            _rb.AddTorque(-_turnRate, ForceMode2D.Force);
        }
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            _rb.AddTorque(_turnRate, ForceMode2D.Force);
        }
    }
}
