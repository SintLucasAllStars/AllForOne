using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    [Header("Customisable Player Stats")]
    public int maxHealth;
    [SerializeField] float currentHealth;
    public int strength;
    public int moveSpeed;
    public int defense;

    [Space]
    [Header("Non-Customisable Stats")]
    [SerializeField] float gravity = 2;
    [SerializeField] float inputSmoothing = 0.2f;

    [Space]
    [Header("Attributes")]
    [SerializeField] bool canMove;

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

    // Update is called once per frame
    void Update()
    {
        // Calls the moveplayer function if canMove
        if (canMove)
            MovePlayer(moveInput);
    }

    private void FixedUpdate()
    {
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
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    // Initialises the player
    void InitializePlayer()
    {
        currentHealth = maxHealth;
    }

    // Movement input
    public void OnMove(InputValue input)
    {
        moveInput = input.Get<Vector2>();
    }
}
