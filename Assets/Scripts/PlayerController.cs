using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    public bool walkOn;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (walkOn)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //Vector3 targetDirection = new Vector3(moveHorizontal, 0f, moveVertical);
            //targetDirection = Camera.main.transform.TransformDirection(targetDirection);
            //targetDirection.y = 0.0f;

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed * 2.5f;
            }
            else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
            }
            else if (Input.GetKey("s"))
            {
                transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
            }

            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * speed;
            }
            else if (Input.GetKey("d") && !Input.GetKey("a"))
            {
                transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * speed;
            }

            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);

            //this.transform.position += Vector3.Normalize(targetDirection);
            //rb.AddForce(targetDirection * speed);
        }
    }
}
