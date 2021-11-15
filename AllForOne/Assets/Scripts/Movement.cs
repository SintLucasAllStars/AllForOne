using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public Transform PlayerCam;

    //##MOVEMENT##
    private float WalkSpeed = 12;
    private float stepOffset;

    private Vector3 velocity;
    private float gravity = -20f;
    private CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;
    private float groundDistance = 0.4f;

    private bool isGrounded;
    private float jumpHeight = 4.3f;

    private float TurnSmoothVelocity;
    private float TurnSmoothTime = 0.08f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        stepOffset = controller.stepOffset;
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
}
