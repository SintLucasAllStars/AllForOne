using System;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private string[] names = { "William","Marshal","Jonas","o'Hara","Johnson","Samuel","Hendriksen","Charles", "Winston", "Frank", "Henry", "Edward", "James", "Thomas", "George" };

    public int id;
    public string unitName;
    public float health;
    public float speed;
    public float strength;
    public float defence;
    public float cost;

    public bool isFortified;
    public bool isActive;
    public MonoBehaviour shit;

    public Weapon currentWeapon;
    private float cooldown;

    private void Start()
    {
        GameManager.instance.activePlayer.units.Add(this);
        unitName = Rank() + " " + GetName();
        id = GameManager.instance.players[0].units.Count + GameManager.instance.players[1].units.Count;
    }

    private void Update()
    {
        if (isActive)
        {
            Playing();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isFortified = true;
            isActive = false;
        }
    }

    private void OnCollisionEnter(Collision cld)
    {
        if (cld.gameObject.GetComponent<Weapon>())
        {
            currentWeapon = cld.gameObject.GetComponent<Weapon>();
        }
    }

    private void Playing()
    {
        WeaponCooldown();
        if (Input.GetButtonDown(buttonName:"Fire1") && cooldown >= 10)
        {
            if (Physics.Raycast(transform.position,Vector3.forward, out RaycastHit hit,currentWeapon.range) && hit.collider.GetComponent<Unit>())
            {
                Unit hitUnit = hit.collider.GetComponent<Unit>();
                Shoot(hitUnit);
            }
            cooldown = 0;
        }
    }

    public void WeaponCooldown() 
    {
        if (cooldown < 10)
        {
            cooldown += 1 * currentWeapon.speed * Time.deltaTime;
        }
    }

    public void Shoot(Unit target) 
    {
        if (!target.isFortified)
        {
            target.health -= ((currentWeapon.damage + strength) / target.defence);
        } else {
            target.health -= (currentWeapon.damage + strength);
        }
    }

    private string Rank()
    {
        if (cost < 25)
        {
            return "Pvt.";
        }
        else if (cost >= 35 && cost < 50)
        {
            return "Pfc.";
        }
        else if (cost >= 50 && cost < 65)
        {
            return "Sgt.";
        }
        else if (cost >= 65 && cost < 75)
        {
            return "Ssg.";
        }
        else {
            return "Sma.";
        }
        return "John Doe";
    }
    private string GetName() {
        return names[UnityEngine.Random.Range(0, names.Length)];
    }
}