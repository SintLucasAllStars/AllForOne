using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    [Header("Stats")]
    public bool inControl = false;
    public int health;
    public int speed;
    public int strenght;
    public int defense;

    private Animator anim;
    private CharacterController cc;
    private bool canJump = true;
    private bool attackReady = true;

    [Header("CameraRef")]
    public GameObject cameraObject;
    public bool rotateToCamera;

    [Header("Weapon")]
    public weapons currentWeapon;
    public enum weapons { noWeapon, powerPunch, knife, warHammer, gun }

    [Header("Team")]
    public Team currentTeam;
    public enum Team { BlueTeam, RedTeam }

    private void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (inControl == true)
        {
            Movement();
            Attack();
            Jump();
            if (rotateToCamera)
            {
                RotateTowardsCamera();
            }
        }
    }

    public void GainControl()
    {
        inControl = true;
        cameraObject.GetComponent<Camera>().enabled = true;
        rotateToCamera = true;
    }

    public void TimersUp()
    {
        inControl = false;
        cameraObject.GetComponent<Camera>().enabled = false;
        rotateToCamera = true;
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
        var CharacterRotation = cameraObject.transform.rotation;
        CharacterRotation.x = 0;
        CharacterRotation.z = 0;
        transform.rotation = CharacterRotation;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 1)
        {
            anim.Play("Death");
            cc.enabled = false;
        }
    }

}
