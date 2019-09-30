using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownView : MonoBehaviour
{
    public Text costText;

    [Header("Sliders")]
    public Slider health;
    public Slider speed;
    public Slider strenght;
    public Slider defense;

    [Header("Stats")]
    public float hp;
    public float sp;
    public float str;
    public float de;

    [Header("Price and Money")]
    public int points;
    public float cost;
    
    [Header("UI Elements")]
    public bool hiring;
    public Button hireButton;
    public GameObject hireMenu;
    
    void Start()
    {
        
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
    }

    public void Hire()
    {
        hireMenu.SetActive(false);
    }

    public void CancelHiring()
    {
        hireMenu.SetActive(true);
        ResetMenu();
    }

    public void CheckPrice()
    {
        hp = health.value * 1.3f;
        sp = speed.value * 1.3f;
        str = strenght.value;
        de = defense.value;
        cost = hp + sp + str + de;
        cost = Mathf.RoundToInt(cost);
        costText.text = cost.ToString();
        if (points >= cost)
        {
            hiring = true;
            hireButton.interactable = true;
        }
        else
        {
            hiring = false;
            hireButton.interactable = false;
        }
    }
    
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
            else if (Physics.Raycast(ray, out hit))
            {
                GameObject spawnedUnit = Instantiate(unit, hit.transform.position, hit.transform.rotation);
                UnitController unitref = spawnedUnit.GetComponent<UnitController>();

                unitref.health = Mathf.RoundToInt(health.value);
                unitref.speed = Mathf.RoundToInt(speed.value);
                unitref.strenght = Mathf.RoundToInt(strenght.value);
                unitref.defense = Mathf.RoundToInt(defense.value);

                Debug.Log("PlaceUnit");
                return;
            }
        }
    }

}
