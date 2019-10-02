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
    private bool canJump = true;
    private bool attackReady = true;

    [Header("CameraRef")]
    public Transform cameraTransform;
    public bool rotateToCamera;

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
        Attack();
        Jump();
        if (rotateToCamera)
        {
            RotateTowardsCamera();
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump && attackReady)
        {
            anim.Play("Jump");
            canJump = false;
        }
    }
    
    void ResetJump()
    {
        canJump = true;
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && attackReady && canJump)
        {
            switch (currentWeapon)
            {
                case weapons.noWeapon:
                    anim.Play("Punch");
                    break;
                case weapons.powerPunch:
                    anim.Play("PowerKick");
                    break;
                case weapons.knife:
                    anim.Play("Knife");
                    break;
                case weapons.warHammer:
                    anim.Play("Hammer");
                    break;
                case weapons.gun:
                    anim.Play("Shoot");
                    break;
                default:
                    break;
            }
            attackReady = false;
        }
    }

    void ResetAttack()
    {
        attackReady = true;
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", z);
    }

    void RotateTowardsCamera()
    {
        var CharacterRotation = cameraTransform.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        transform.rotation = CharacterRotation;
    }

}
