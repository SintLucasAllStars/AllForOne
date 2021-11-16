using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Transform PlayerCam;

    //##MOVEMENT##
    public float WalkSpeed = 12;
    private float stepOffset;

    private Vector3 velocity;
    private float gravity = -20f;
    private CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    private float groundDistance = 0.4f;

    public bool isRunBoost;
    public float PowerupTimer;

    private bool isGrounded;
    private float jumpHeight = 4.3f;

    private float TurnSmoothVelocity;
    private float TurnSmoothTime = 0.08f;
    Vector3 offset;

    RaycastHit hit;
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stepOffset = controller.stepOffset;
    }

    private void Update()
    {
        //attack player
        offset = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z); 

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Physics.Raycast(offset, transform.forward, out hit, 3) && hit.transform.CompareTag("p1") || hit.transform.CompareTag("p2"))
            {
                Destroy(hit.transform.gameObject);
            }
        }

        if (isRunBoost) RunPowerup();
        Debug.DrawRay(offset, transform.forward);
    }

    private void FixedUpdate()
    {

        //#### GRAVITY ####
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //######## WALKING #########
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        //###### ON THE GROUND MOVEMENT #######
        if (inputDir != Vector2.zero)
        {
            float playerRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + PlayerCam.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, playerRotation, ref TurnSmoothVelocity, TurnSmoothTime);

            controller.Move(transform.forward * WalkSpeed * Time.deltaTime);
        }

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1.5f;
            controller.stepOffset = stepOffset;
        }

        controller.Move(velocity * Time.deltaTime);

        //######## JUMP #########
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-1f * jumpHeight * gravity);
            controller.stepOffset = 0;
        }
    }

    public void RunPowerup()
    {
        PowerupTimer -= Time.deltaTime;

        if (PowerupTimer >= 0) { WalkSpeed = 24; }
        else { WalkSpeed = 12; isRunBoost = false; }
    }
}
