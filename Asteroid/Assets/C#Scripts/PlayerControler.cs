using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    
    [SerializeField] private float forwardThrust = 2f;
    [SerializeField] private float backwardThrust = 1f;
    [SerializeField] private float lateralThrust = 1f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float velocityDeceleration = 0.99f;

    [SerializeField] private float rotationBurst = 1f;
    [SerializeField] private float rotationAcceleration = 15f;
    [SerializeField] private float maxRotation = 10f;
    [SerializeField] private float radialDeceleration = 0.95f;


    private Rigidbody2D rb;
    float degrees = 0f;
    float rotationSpeed = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {




        
        if (Input.GetKeyDown(KeyCode.A)) { rotationSpeed += (rotationBurst); }
        if (Input.GetKeyDown(KeyCode.D)) { rotationSpeed -= (rotationBurst); }

        if (Input.GetKey(KeyCode.A))
        { rotationSpeed += rotationAcceleration * Time.deltaTime; }
        if (Input.GetKey(KeyCode.D))
        { rotationSpeed -= rotationAcceleration * Time.deltaTime; }
        { rotationSpeed = rotationSpeed * radialDeceleration; }

        if (Mathf.Abs(rotationSpeed) > maxRotation)
        { rotationSpeed = Mathf.Sign(rotationSpeed) * maxRotation; }
        else if ((Mathf.Abs(rotationSpeed) < maxRotation * 0.005) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        { rotationSpeed = 0f; }

        degrees += rotationSpeed;
        transform.rotation = Quaternion.Euler(0f, 0f, degrees);


        if (Input.GetKey(KeyCode.W))
        {
            Vector2 force = transform.up * forwardThrust;
            rb.AddForce(force);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            Vector2 force = transform.right * -lateralThrust;
            rb.AddForce(force);
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector2 force = transform.up * -backwardThrust;
            rb.AddForce(force);
        }
        if (Input.GetKey(KeyCode.E))
        {
            Vector2 force = transform.right * lateralThrust;
            rb.AddForce(force);
        }


        rb.linearVelocity = rb.linearVelocity * velocityDeceleration;


        if (rb.linearVelocity.magnitude > maxSpeed)
        { rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed; }
        else if (rb.linearVelocity.magnitude < maxSpeed*0.005)
        { rb.linearVelocity = new Vector2(0f,0f); }


            Debug.Log(Time.deltaTime);

    }
}
