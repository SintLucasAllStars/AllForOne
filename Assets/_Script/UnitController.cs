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

    [Header("Fortify")]
    public GameObject shield;
    public bool fortified;

    [Header("Weapon")]
    public int weaponDamage;
    public int weaponRange;
    public weapons currentWeapon;
    public enum weapons { noWeapon, powerPunch, knife, warHammer, gun }

    [Header("TeamTag")]
    public string teamName;

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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                shield.SetActive(true);
                fortified = true;
                StopCoroutine("Timer");
                TimersUp();
            }
        }
    }

    public void GainControl()
    {
        fortified = false;
        shield.SetActive(false);
        inControl = true;
        cameraObject.GetComponent<Camera>().enabled = true;
        rotateToCamera = true;
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10f);
        TimersUp();
    }

    public void TimersUp()
    {
        inControl = false;
        cameraObject.GetComponent<Camera>().enabled = false;
        rotateToCamera = false;
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        GameManager.instance.TurnEnded();
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
                    weaponDamage = 2 + strenght;
                    weaponRange = 1;
                    break;
                case weapons.powerPunch:
                    anim.Play("PowerKick");
                    weaponDamage = 8 + strenght;
                    weaponRange = 1;
                    break;
                case weapons.knife:
                    anim.Play("Knife");
                    weaponDamage = 15 + strenght;
                    weaponRange = 1;
                    break;
                case weapons.warHammer:
                    anim.Play("Hammer");
                    weaponDamage = 40 + strenght;
                    weaponRange = 2;
                    break;
                case weapons.gun:
                    anim.Play("Shoot");
                    weaponDamage = 10 + strenght;
                    weaponRange = 10;
                    break;
                default:
                    break;
            }
            attackReady = false;
        }
    }

    public void AttackRay()
    {
        RaycastHit hit;

        Vector3 rayOrigin = transform.position + transform.forward * 0.5f + transform.up * 0.5f;
        Debug.DrawRay(rayOrigin, transform.forward * weaponRange, Color.blue, 1);
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, weaponRange))
        {
            if (hit.collider.tag != teamName)
            {
                Debug.Log("Raycast hit " + hit.collider.name);
                UnitController ut = hit.collider.GetComponent<UnitController>();
                if (ut != null)
                {
                    ut.TakeDamage(weaponDamage);
                }
            }
        }
    }

    void ResetAttack()
    {
        attackReady = true;
    }

    void Movement()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

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
        if (fortified == false)
        {
            health -= damage;
        }
        else
        {
            int calcDamaged = Mathf.RoundToInt(damage / defense);
            health -= calcDamaged;
        }
        if (health < 1)
        {
            anim.Play("Death");
            cc.enabled = false;
            if (teamName == "Blue Team")
            {
                GameManager.instance.teamBlue--;
                GameManager.instance.CheckIfWon();
            }
            else if (teamName == "Red Team")
            {
                GameManager.instance.teamRed--;
                GameManager.instance.CheckIfWon();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Death"))
        {
            TakeDamage(1000);
        }
    }
}
