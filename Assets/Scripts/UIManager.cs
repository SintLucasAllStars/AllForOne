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
    public int health;
    public Text speedT;
    public int speed;
    public Text strengthT;
    public int strength;
    public Text defenceT;
    public int defence;
    public int cost;

    #region BTN Funcs 

    public void AddValue(int _changeVal)
    {
        switch (_changeVal)
        {
            case 0:
                health++;
                healthT.text = health.ToString();
                break;
            case 1:
                speed++;
                speedT.text = speed.ToString();
                break;
            case 2:
                strength++;
                strengthT.text = strength.ToString();
                break;
            case 3:
                defence++;
                defenceT.text = defence.ToString();
                break;
            default:
                Debug.Log("Add ChangeValue Switch sent to Default, ChangeVal: " + _changeVal);
                break;
        }
        cost = (health * 4) + (speed * 4) + strength + defence;
    }
    public void DecValue(int _changeVal)
    {
        switch (_changeVal)
        {
            case 0:
                if (health <= 0)
                {
                    health--;
                    healthT.text = health.ToString();
                }
                break;
            case 1:
                if (health <= 0)
                {
                    speed--;
                    speedT.text = speed.ToString();
                }
                break;
            case 2:
                if (strength <= 0)
                {
                    strength--;
                    strengthT.text = strength.ToString();
                }
                break;
            case 3:
                if (defence <= 0)
                {
                    defence--;
                    defenceT.text = defence.ToString();
                }
                break;
            default:
                Debug.Log("Dec ChangeValue Switch sent to Default, ChangeVal: " + _changeVal);
                break;
        }
    }

    public void BuyUnit() {

        if (GameManager.instance.activePlayer.money >= cost)
        {
            // buy
        }
        else {
            // don't buy
        }

    }
    
    #endregion

}