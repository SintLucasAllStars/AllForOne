using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public int speed;
    public int strenght;
    public int defense;

    public float jumpHeight;

    private Animator anim;
    private Rigidbody rb;

    [Header("Weapon")]
    public weapons currentWeapon;
    public enum weapons { noWeapon, powerPunch, knife, warHammer, gun }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpHeight);
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", z);
    }

}
