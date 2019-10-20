using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [Header("UI Assignables")]
    public GameObject purchaseUI;
    public Transform blueUnits;
    public Transform redUnits;

    [Header("Purchase UI values")]
    public Text healthT;
    public int health = 1;
    public Text speedT;
    public int speed = 1;
    public Text strengthT;
    public int strength = 1;
    public Text defenceT;
    public int defence = 1;
    public int cost = 10;
    public Text costText;

    public bool placing = false;
    public GameObject unit;

    private void Update()
    {
        if (placing && Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlaceUnit();
        }
    }

    public void AddValue(int _changeVal)
    {
        switch (_changeVal)
        {
            case 0:
                if (health < 10)
                {
                    health++;
                    healthT.text = health.ToString();
                }
                break;
            case 1:
                if (speed < 10)
                {
                    speed++;
                    speedT.text = speed.ToString();
                }
                break;
            case 2:
                if (strength < 10)
                {
                    strength++;
                    strengthT.text = strength.ToString();
                }
                break;
            case 3:
                if (defence < 10)
                {
                    defence++;
                    defenceT.text = defence.ToString();
                }
                break;
            default:
                Debug.Log("Add ChangeValue Switch sent to Default, ChangeVal: " + _changeVal);
                break;
        }
        cost = 10 + ((health - 1) * 4) + ((speed - 1) * 4) + (strength - 1) + (defence - 1);
        costText.text = "Cost: " + cost.ToString();
    }
    public void DecValue(int _changeVal)
    {
        switch (_changeVal)
        {
            case 0:
                if (health > 1)
                {
                    health--;
                    healthT.text = health.ToString();
                }
                break;
            case 1:
                if (speed > 1)
                {
                    speed--;
                    speedT.text = speed.ToString();
                }
                break;
            case 2:
                if (strength > 1)
                {
                    strength--;
                    strengthT.text = strength.ToString();
                }
                break;
            case 3:
                if (defence > 1)
                {
                    defence--;
                    defenceT.text = defence.ToString();
                }
                break;
            default:
                Debug.Log("Dec ChangeValue Switch sent to Default, ChangeVal: " + _changeVal);
                break;
        }
        cost = 10 + ((health - 1) * 4) + ((speed - 1) * 4) + (strength -1) + (defence - 1);
        costText.text = "Cost: " + cost.ToString();
    }

    public void BuyUnit() {

        if (GameManager.instance.activePlayer.money >= cost) {
            GameManager.instance.activePlayer.money -= cost;
            PlacingPhase();
        }
        else {
            // don't buy
        }

    }

    public void PlacingPhase() {
        purchaseUI.SetActive(false);
        placing = true;
    }

    public void PlaceUnit() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin,Vector3.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject placedUnit = Instantiate(unit,hit.point,Quaternion.identity);
            Unit newUnit = placedUnit.GetComponent<Unit>();
            AssignValues(newUnit);

            placing = false;
            purchaseUI.SetActive(true);
            GameManager.instance.EndTurn();
        }
    }

    private void AssignValues(Unit thisUnit) {
        thisUnit.health = health;
        thisUnit.speed = speed;
        thisUnit.strength = strength;
        thisUnit.defence = defence;
        thisUnit.cost = cost;
    }

}