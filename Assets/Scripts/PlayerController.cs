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

    public float e_health, e_strength, e_defense = 1f;

    public int rayDistance;

    RaycastHit hit;

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
            if (e_health <= 0)
            {
                Destroy(hit.transform.gameObject);
                e_health = 1;
            }
            else
            {
                
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.transform.tag == "u_Player2")
                {
                    Debug.Log("Attack");
                    e_health = hit.transform.gameObject.GetComponent<UnitValues>().health;
                    e_strength = hit.transform.gameObject.GetComponent<UnitValues>().strength;
                    e_defense = hit.transform.gameObject.GetComponent<UnitValues>().defense;

                    e_health -= (this.GetComponent<UnitValues>().strength / e_defense);

                    hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
                }
                else if (hit.transform.tag == "u_Player1")
                {
                    Debug.Log("Attack");
                    e_health = hit.transform.gameObject.GetComponent<UnitValues>().health;
                    e_strength = hit.transform.gameObject.GetComponent<UnitValues>().strength;
                    e_defense = hit.transform.gameObject.GetComponent<UnitValues>().defense;

                    e_health -= (this.GetComponent<UnitValues>().strength / e_defense);

                    hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
                }
            }
        }

        if (GameObject.FindGameObjectsWithTag("u_Player1").Length == 0 && walkOn)
        {
            Debug.Log("Player 2 WINS!");
        }

        if (GameObject.FindGameObjectsWithTag("u_Player2").Length == 0 && walkOn)
        {
            Debug.Log("Player 1 WINS!");
        }
    }
}
