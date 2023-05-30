using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float turnRate;
    [SerializeField] private Animation _animation;

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
            _rb.AddForce(transform.up * speed, ForceMode2D.Force);
            _animation.Play();
        }

        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            _rb.AddTorque(-turnRate, ForceMode2D.Impulse);
        }
        
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            _rb.AddTorque(turnRate, ForceMode2D.Impulse);
        }
    }
}
