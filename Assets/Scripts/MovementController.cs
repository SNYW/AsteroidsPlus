using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private Vector2 maxBounds;
    [SerializeField] private float speed;
    [SerializeField] private float turnRate;

    private Rigidbody2D _rb;
    private LineRenderer _lr;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponentInChildren<LineRenderer>();
    }

    void Update()
    {
        ManageMovement();
        transform.UpdateScreenWrap();
    }


    private void ManageMovement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            _rb.AddForce(transform.up * speed, ForceMode2D.Force);
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            _rb.AddTorque(-turnRate, ForceMode2D.Force);
        }
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            _rb.AddTorque(turnRate, ForceMode2D.Force);
        }
    }
}
