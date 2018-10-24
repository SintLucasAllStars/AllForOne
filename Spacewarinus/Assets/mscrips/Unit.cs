using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    [Header("Stats")]
    public float health;
    public float strenght;
    public float defence;
    public float speed;
    [Header("Team")]
    public int team;

    public float currentTime = 10f;
    public bool isAiming, isFortified, isPlaying;
    public Texture2D crosshairImage;
    public TeamManager tm;
    private WeaponType currentWeapon;

    // Use this for initialization
    void Start()
    {
        currentWeapon = new WeaponType();
        currentWeapon.weaponName = "hands";
        currentWeapon.damage = 1;
        currentWeapon.speed = 10;
        currentWeapon.range = 5;
        tm = FindObjectOfType<TeamManager>();
    }

    // Update is called once per frame
    void Update()
    {
        OnPlay();
    }
    private void OnPlay()
    {
        if (isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnAttack();
            }

            if(currentTime >= 0)
            {
                currentTime -= Time.deltaTime;
            }
            else
            {
                OnDeSelected();
            }
        }
    }
    public void OnSelected()
    {
        currentTime = 10f;
        isPlaying = true;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = true;
        GetComponent<Movement>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void OnDeSelected()
    {
        isPlaying = false;
        GetComponent<UnityStandardAssets.Characters.FirstPerson.RigidbodyFirstPersonController>().enabled = false;
        GetComponent<Movement>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
        tm.ChangeTeam();
        transform.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
    }
    public void SetUnitStats(float[] stats)
    {
        health = stats[0];
        strenght = stats[1];
        defence = stats[2];
        speed = stats[3];
    }
    public void OnAttack()
    {
        RaycastHit hit;
        Ray ray = transform.GetChild(0).GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (Physics.Raycast(ray, out hit,currentWeapon.range))
        {
            Transform hitTransform = hit.transform;
            if (hit.transform.GetComponent<Unit>() && hit.transform.GetComponent<Unit>().team != team)
            {
                Unit u = hit.transform.GetComponent<Unit>();
                u.health = -(currentWeapon.damage * strenght);
                Debug.Log(u.gameObject.name + " is hit");
                u.IsHit();
            }

        }

    }
    public void IsHit()
    {
        if(health <= 0)
        {
            tm.players[team].units.Remove(GetComponent<Unit>());
            Destroy(gameObject);
        }
    }
    private void OnGUI()
    {
        float xMin = (Screen.width / 2) - (crosshairImage.width / 2);
        float yMin = (Screen.height / 2) - (crosshairImage.height / 2);
        if (isPlaying)
        {
            GUI.DrawTexture(new Rect(xMin, yMin, crosshairImage.width, crosshairImage.height), crosshairImage);
        }
    }

}
