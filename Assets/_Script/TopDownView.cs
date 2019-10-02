using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownView : MonoBehaviour
{
    public Text costText;

    [Header("Sliders")]
    public Slider health;
    public Text healthText;
    public Slider speed;
    public Text speedText;
    public Slider strenght;
    public Text strenghtText;
    public Slider defense;
    public Text defenseText;

    [Header("Stats")]
    public float hp;
    public Text hpText;
    public float sp;
    public Text spText;
    public float str;
    public Text strText;
    public float de;
    public Text deText;

    [Header("Price and Money")]
    public int points;
    public Text pointsText;
    public float cost;
    
    [Header("UI Elements")]
    public bool nextTeam;
    public bool hiring;
    public Button hireButton;
    public GameObject hireMenu;
    public GameObject nextHireMenu;

    void Start()
    {
        pointsText.text = points.ToString();
        CheckPrice();
    }
    
    void Update()
    {
        if (hiring == true)
        {
            PlaceUnits();
        }
    }

    void ResetMenu()
    {
        health.value = 0;
        speed.value = 0;
        strenght.value = 0;
        defense.value = 0;
        CheckPrice();
    }

    public void Hire()
    {
        hiring = true;
        points -= Mathf.RoundToInt(cost);
        pointsText.text = points.ToString();
        hireMenu.SetActive(false);
    }

    public void BackToHiring()
    {
        hiring = false;
        hireMenu.SetActive(true);
        ResetMenu();
    }

    void CheckTurn()
    {
        if (points < 10)
        {
            if (nextTeam == true)
            {
                nextHireMenu.SetActive(true);
                hireMenu.SetActive(false);
            }
        }
        else
        {
            BackToHiring();
        }
    }

    public void CheckPrice()
    {
        hp = health.value * 4f;
        sp = speed.value * 4f;
        str = strenght.value;
        de = defense.value;
        healthText.text = hp.ToString();
        speedText.text = sp.ToString();
        strenghtText.text = str.ToString();
        defenseText.text = de.ToString();
        cost = hp + sp + str + de;
        cost = Mathf.RoundToInt(cost);
        costText.text = cost.ToString();
        if (points >= cost)
        {
            hireButton.interactable = true;
        }
        else
        {
            hireButton.interactable = false;
        }
    }

    [Header("Unit Prefab")]
    public LayerMask placeableLayer;
    public GameObject unit;
    void PlaceUnits()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            else if (Physics.Raycast(ray, out hit, placeableLayer))
            {
                GameObject spawnedUnit = Instantiate(unit, hit.point, hit.transform.rotation);
                UnitController unitref = spawnedUnit.GetComponent<UnitController>();

                unitref.health = Mathf.RoundToInt(health.value);
                unitref.speed = Mathf.RoundToInt(speed.value);
                unitref.strenght = Mathf.RoundToInt(strenght.value);
                unitref.defense = Mathf.RoundToInt(defense.value);

                Debug.Log("PlaceUnit");
                CheckTurn();
                return;
            }
        }
    }

}
