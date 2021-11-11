using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Customisable Player Stats")]
    public Unit unitStats = new Unit(10, 10, 10, 10);
    [SerializeField] float currentHealth;

    [Space]
    [Header("Non-Customisable Stats")]
    [SerializeField] float gravity = 2;
    [SerializeField] float inputSmoothing = 0.2f;
    [SerializeField] float turningSpeed = 4.5f;

    [Space]
    [Header("Attributes")]
    public bool canMove;

    [Space]
    [Header("Input")]
    Vector2 moveInput;
    Vector2 currentInputVector;
    Vector2 inputVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Initialise the player
        InitializePlayer();
    }

    private void Update()
    {
        if (canMove)
        {
            // For rotating the player
            if (moveInput != Vector2.zero)
            {
                Vector3 cameraDirection = transform.position - Camera.main.transform.position;
                cameraDirection.y = 0;
                cameraDirection.Normalize();
                transform.LookAt(transform.position + cameraDirection);
            }
        }
    }

    private void FixedUpdate()
    {
        // Calls the moveplayer function if canMove
        if (canMove)
            MovePlayer(moveInput);

        // Apply gravity if going down
        if (rb.velocity.y < 0.1f)
        {
            rb.AddForce(Vector3.down * gravity * Time.deltaTime);
        }
    }

    // Moves the player according to input
    void MovePlayer(Vector2 input)
    {
        currentInputVector = Vector2.SmoothDamp(currentInputVector, input, ref inputVelocity, inputSmoothing);
        Vector3 movement = new Vector3(currentInputVector.x, 0, currentInputVector.y);
        transform.Translate(movement * (unitStats.GetSpeed() / 4) * Time.deltaTime);
    }

    // Initialises the player
    void InitializePlayer()
    {
        currentHealth = unitStats.GetHealth();
    }

    // Movement input
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }
}
