using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerCharacter : MonoBehaviour
{
    public GameManager gm;
    public UiManager uiM;
    public StatList stats = new StatList();
    public GameObject redMarker;
    public GameObject blueMarker;
    public bool active = false;
    public float walkSpeed = 10;

    public FirstPersonController controller;
    public GameObject weaponObject;
    public Weapon weaponStats;
    public GameObject defaultWeapon;
    public Transform rayOrigin;



    // Start is called before the first frame update
    void Start()
    {
        rayOrigin = gameObject.transform.GetChild(4);
        gm = GameObject.Find("Managers").GetComponent<GameManager>();
        uiM = GameObject.Find("Managers").GetComponent<UiManager>();
        
        weaponObject = gm.weaponList[uiM.weaponId];
        PlaceWeapon();
        weaponStats = weaponObject.GetComponent<Weapon>();
        controller = gameObject.GetComponent<FirstPersonController>();
        controller.enabled = !controller.enabled;
        if (stats.team == "Red")
        {
            gameObject.tag = "Red";
        }
        else
        {
            gameObject.tag = "Blue";
        }
        AssignMarkers();
        stats.DebugStats();
    }

    public void PlaceWeapon()
    {
        GameObject weapon = Instantiate(weaponObject, gameObject.transform.GetChild(3).transform.position, gameObject.transform.GetChild(3).transform.rotation);
        weapon.transform.parent = gameObject.transform;
    }

    public void AssignMarkers()
    {
        redMarker = gameObject.transform.GetChild(0).gameObject;
        blueMarker = gameObject.transform.GetChild(1).gameObject;
        redMarker.SetActive(false);
        blueMarker.SetActive(false);
    }

    public void TakeControl()
    {
        controller.enabled = !controller.enabled;
    }

    public void ActivateMarker(bool markerActive)
    {
        if (markerActive == true)
        {
            if (stats.team == "Red")
            {
                redMarker.SetActive(true);
            }
            else
            {
                blueMarker.SetActive(true);
            }  
        }
        else
        {
            if (stats.team == "Red")
            {
                redMarker.SetActive(false);
            }
            else
            {
                blueMarker.SetActive(false);
            }  
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (active == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
        }
    }

    public void Attack()
    {
        Ray weaponRay = new Ray(rayOrigin.position,rayOrigin.TransformDirection(Vector3.forward));
        RaycastHit hit;
        if (Physics.Raycast(weaponRay, out hit, weaponStats.range + 10));
        {

            if (hit.collider.gameObject.CompareTag("Blue"))
            {
                hit.collider.gameObject.GetComponent<PlayerCharacter>().TakeDamage(stats.strength + weaponStats.dmg);
            }

            if (hit.collider.gameObject.CompareTag("Red"))
            {
                hit.collider.gameObject.GetComponent<PlayerCharacter>().TakeDamage(stats.strength + weaponStats.dmg);
            }
        }

    }

    public void TakeDamage(int dmg)
    {
        stats.health = stats.health - dmg;
        if (stats.health <= 0)
        {
            if (gameObject.CompareTag("Red"))
            {
                gm.redPlayerUnits.Remove(this.gameObject);
            }
            else
            {
                gm.bluePlayerUnits.Remove(this.gameObject);
            }
            Destroy(this.gameObject);
        }
    }


}
