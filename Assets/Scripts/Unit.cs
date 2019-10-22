using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

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

    public Weapon currentWeapon;
    private float cooldown;

    public Powerup activePowerup;

    public GameObject selfCam;

    private float gameTimer;

    private void Start()
    {
        GameManager.instance.activePlayer.units.Add(this);
        unitName = Rank() + " " + GetName();
        id = GameManager.instance.players[0].units.Count + GameManager.instance.players[1].units.Count;
    }

    private void Update()
    {
        if (isActive && GameManager.instance.state == GameState.Playing)
        {
            selfCam.SetActive(true);
        }
        else
        {
            selfCam.SetActive(false);
        }


        if (isActive)
        {
            GetComponent<FirstPersonController>().enabled = true;
            GetComponent<CharacterController>().enabled = true;
            //Camera.main.gameObject.SetActive(false);
            Playing();
        }
        else {
            GetComponent<FirstPersonController>().enabled = false;
            GetComponent<CharacterController>().enabled = false;
            if (Camera.main != null)
            {
                //Camera.main.gameObject.SetActive(true);
            }
            //selfCam.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision cld)
    {
        if (cld.gameObject.GetComponent<Weapon>())
        {
            currentWeapon = cld.gameObject.GetComponent<Weapon>();
        }
        if (cld.gameObject.GetComponent<Powerup>())
        {
            activePowerup = cld.gameObject.GetComponent<Powerup>();
        }
    }

    private void Playing()
    {
        GameTimer();
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFortified = true;
            isActive = false;
        }

        WeaponCooldown();
        if (Input.GetButtonDown(buttonName:"Fire1") && cooldown >= 5)
        {
            if (Physics.Raycast(transform.position,Vector3.forward, out RaycastHit hit,currentWeapon.range) && hit.collider.GetComponent<Unit>())
            {
                Unit hitUnit = hit.collider.GetComponent<Unit>();
                Shoot(hitUnit);
            }
            cooldown = 0;
        }
    }

    public void GameTimer() {
        if (gameTimer < 10)
        {
            gameTimer += 1 * Time.deltaTime;
        }
        else if (gameTimer >= 10)
        {
            isActive = false;
            GameManager.instance.EndTurn();
            //GameManager.instance.state = GameState.UnitSelection;
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