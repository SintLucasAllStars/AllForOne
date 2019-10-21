using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Unit : MonoBehaviour
{
    public enum Weapontype { punch, superPunch, knife, slash, gun}
    public Weapontype weapontype;
    public List<Sprite> weapons;
    public Image weaponImg;
    [Header("Stats")]
    public int health;
    public int strength;
    public int defence;
    public int speed;
    public int rotSpeed;
    public Camera cam;
    bool fortify = false;
    public TextMeshProUGUI healthTxt, speedTxt, strengthTxt, defenceTxt;
    public TextMesh Text3d;

    public bool isSelected = false;
    

    [Header("Refs")]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GetHit(int damage)
    {
        if (fortify)
            health -= damage - defence;
        else
        {
            health -= damage;
            anim.Play("hit");
        }

        if (health > 0)
        {
            Debug.Log("Health left is = " + health);
        }
        else
        {
            Debug.Log("Unit has no health left");
            Die();
        }
    }


    private void Update()
    {
        //Text3d.text = "Health = " + speed.ToString();
        if (isSelected)
        {
            CharacterMovement();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Attack();
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                SetFortify();
            }
        }
    }

    public void Die()
    {
        Debug.Log("this player died there was no roof above him");
        Destroy(this.gameObject);
        Gamemanager.instance.CheckForUnits();
    }

    private void OnMouseOver()
    {
        if(this.tag == Gamemanager.instance.currentplayer.teamColor)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Gamemanager.instance.topview.transform.localPosition = Vector3.MoveTowards(gameObject.transform.localPosition, new Vector3(cam.transform.localPosition.x, 0f, cam.transform.localPosition.z), 10 * Time.deltaTime);
                OnSelected();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("You cant control another players unit you cheater");
            }
        }
    }

    public void OnSelected()
    {
        anim.Play("Idle/Walk");
        isSelected = true;
        cam.gameObject.SetActive(true);
        Gamemanager.instance.TopViewTurnOn();
        SetUnitInfoText();
        anim.speed = speed;
        StartCoroutine(Timer());
    }

    void CharacterMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        this.gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotSpeed);
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
        Debug.Log(anim.speed + "this is the speed of the animator");
        anim.speed = 1;
        anim.Play(weapontype.ToString());
        AttackRay(tRange, (tDamage * strength));
        anim.speed = speed;
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
        EndTurn();
    }

    public void EndTurn()
    {
        isSelected = false;
        anim.SetFloat("Horizontal", 0);
        anim.SetFloat("Vertical", 0);
        cam.gameObject.SetActive(false);
        Gamemanager.instance.SwitchPlayer();
        Gamemanager.instance.TopViewTurnOn();
        //CheckIfInside();
    }

    public void SetUnitInfoText()
    {
        healthTxt.text = "Health = " + health.ToString();
        speedTxt.text = "Speed = " + speed.ToString();
        strengthTxt.text = "Strength = " + strength.ToString();
        defenceTxt.text = "Defence = " + defence.ToString();
    }

    public void SetFortify()
    {
        anim.Play("Fortify");
        StopAllCoroutines();
        EndTurn();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "powerPunch":
                weapontype = Weapontype.superPunch;
                weaponImg.sprite = weapons[1];
                Destroy(collision.gameObject);
                break;
            case "knife":
                weapontype = Weapontype.knife;
                weaponImg.sprite = weapons[2];
                Destroy(collision.gameObject);
                break;
            case "slash":
                weapontype = Weapontype.slash;
                weaponImg.sprite = weapons[3];
                Destroy(collision.gameObject);
                break;
            case "gun":
                weapontype = Weapontype.gun;
                weaponImg.sprite = weapons[4];
                Destroy(collision.gameObject);
                break;
            //case "Lava":
            //    Die();
            //    break;
        }
    }
}
