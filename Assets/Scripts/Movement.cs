using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public UnityEngine.CharacterController cont;
    
    public float speed = 2f;

    public float turnSmoothness = 0.1f;

    public float gravity = -9f;

    public Transform groundCheck;

    public float groundDistance = 0.3f;

    public LayerMask groundMask;

    public float jumpHeight = 2f;
    
    private Transform cam;

    private float turnSmoothVelocity;

    private Vector3 velocity;

    private bool isGrounded;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        
        if (isGrounded && velocity.y < 0) velocity.y = -1f;

        float hori = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(hori, 0f, vert).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngel = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngel, ref turnSmoothVelocity,
                turnSmoothness);
            
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = (Quaternion.Euler(0f, targetAngel, 0f) * Vector3.forward).normalized;
            
            cont.Move(moveDir * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -1f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        cont.Move(velocity * Time.deltaTime);
    }
}
