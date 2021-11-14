using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
    public Unit unit;
    public GameObject player;
    public CapsuleCollider coll;
    public float extraHeight;
    public float jumpForce;
    public float jumpButtonReleaseDamping;

    public Rigidbody rb;

    //Getters
    public float Health 
    {
        get
        {
            return unit.GetHealth();
        }
        set
        {
            if (unit == null)
            {
                unit = new Unit();
            }
            else
            {
                unit.health = value;
            }
        }
    }
    public float Strength 
    {
        get
        {
            return unit.GetStrength();
        }
        set
        {
            if (unit == null)
            {
                unit = new Unit();
            }
            else
            {
                unit.strength = value;
            }
        }
    }
    public float Speed 
    {
        get
        {
            return unit.GetSpeed();
        }
        set
        {
            if (unit == null)
            {
                unit = new Unit();
            }
            else
            {
                unit.speed = value;
            }
        }
    }
    public float Defence 
    {
        get
        {
            return unit.GetDefence();
        }
        set
        {
            if (unit == null)
            {
                unit = new Unit();
            }
            else
            {
                unit.defence = value;
            }
        }
    }

    private void Start()
    {
        player = GameObject.Find("Player1");

        player.GetComponent<Player>().units.Add(unit);
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float z = Input.GetAxis("Vertical") * Speed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * jumpButtonReleaseDamping);
        }

        transform.Translate(h, 0, z);
    }

    private bool IsGrounded()
    {
        //Vector3 size = new Vector3(coll.center.x - 0.1f, coll.center.y, coll.center.z);
        RaycastHit raycastHit;

        Color rayColor;
        if (Physics.Raycast(transform.position, Vector3.down, out raycastHit, 1f))
        {
            if (raycastHit.collider != null)
            {
                rayColor = Color.green;
            }
            else
            {
                rayColor = Color.red;
                print(raycastHit.collider.name);
            }
        }

        Debug.DrawRay(transform.position, Vector3.down * (transform.position.y + extraHeight), Color.green);
        return raycastHit.collider != null;
    }

    void RemoveUnit()
    {
        //unit is dead
        player.GetComponent<Player>().units.Remove(unit);
    }
}
