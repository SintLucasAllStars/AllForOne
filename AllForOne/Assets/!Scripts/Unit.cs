using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Weapontype { punch, superPunch, knife, slash, gun}
    public Weapontype weapontype;
    [Header("Stats")]
    public int health;
    public int strength;
    public int defence;
    public int speed;
    public Camera cam;

    public bool isSelected = false;
    

    [Header("Refs")]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GetHit(int damage)
    {
        health -= damage;
        if(health > 0)
        {
            Debug.Log("Health left is = " + health);
        }
        else
        {
            Debug.Log("Unit has no health left");
        }
    }


    private void Update()
    {
        if (isSelected)
        {
            CharacterMovement();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
            }
        }
    }

    public void Die()
    {

    }

    private void OnMouseOver()
    {
        
        if (Input.GetMouseButtonDown(0))
        {

            OnSelected();
        }
    }

    public void OnSelected()
    {
        isSelected = true;
        cam.enabled = true;
        StartCoroutine(Timer());
    }

    void CharacterMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        anim.SetFloat("Horizontal", x);
        anim.SetFloat("Vertical", y);
    }

    void Attack()
    {
        int tRange=0;
        int tDamage=0;
        int tSpeed=0;
        switch (weapontype)
        {
            case Weapontype.punch:
                tRange = 1;
                tDamage = 1;
                tSpeed = 10;
                break;
            case Weapontype.superPunch:
                tRange = 1;
                tDamage = 2;
                tSpeed = 10;
                break;
            case Weapontype.knife:
                tRange = 1;
                tDamage = 3;
                tSpeed = 8;
                break;
            case Weapontype.slash:
                tRange = 1;
                tDamage = 8;
                tSpeed = 4;
                break;
            case Weapontype.gun:
                tRange = 1;
                tDamage = 5;
                tSpeed = 3;
                break;
            default:
                break;
        }
        Debug.Log("weaponType = " + weapontype);
        anim.Play(weapontype.ToString());
        AttackRay(tRange, tDamage);
    }

    public void AttackRay(int weaponRange, int Damage)
    {
        RaycastHit hit;

        Vector3 rayOrigin = transform.position + transform.forward * 0.5f + transform.up * 0.5f;
        Debug.DrawRay(rayOrigin, transform.forward * weaponRange, Color.blue, 1);
        if (Physics.Raycast(rayOrigin, transform.forward, out hit, weaponRange))
        {
            if (hit.collider.tag != this.tag)
            {
                Unit unit = hit.collider.GetComponent<Unit>();
                if (unit != null)
                {
                    unit.GetHit(Damage);
                }
            }
        }
    }

    public void CheckIfInside()
    {
        RaycastHit hit;

        Vector3 rayOrigin = transform.position + transform.forward * 0.5f + transform.up * 0.5f;
        Debug.DrawRay(rayOrigin, transform.up * 10, Color.blue, 1);
        if (Physics.Raycast(rayOrigin, transform.forward, out hit))
        {
            if (hit.collider.tag != "Roof")
            {
                Die();
            }
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(10);
        isSelected = false;
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        CheckIfInside();
        this.cam.enabled = false;
        TopView.instance.EnableTopView();
    }
}
