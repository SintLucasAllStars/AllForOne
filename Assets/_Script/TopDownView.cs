using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopDownView : MonoBehaviour
{

    public Slider health;
    public Slider speed;

    public Slider strenght;
    public Slider defense;

    public int points;
    public int cost;

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

    public void Hire()
    {
        hireMenu.SetActive(false);
    }

    public void CancelHiring()
    {
        hireMenu.SetActive(true);
    }

    public void CheckPrice()
    {
        if (points >= (health.value * 1.3f) + (speed.value * 1.3f) + strenght.value + defense.value))
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
