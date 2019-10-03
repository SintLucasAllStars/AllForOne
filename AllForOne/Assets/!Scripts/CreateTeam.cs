using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateTeam : MonoBehaviour
{
    public TextMeshProUGUI healthTxt, speedTxt, defenceTxt, strengthTxt;
    public Unit unitRed, unitBlue;
    bool placeUnit = false;
    public GameObject UI;

    int health;
    int speed;
    int defence;
    int strength;
    int points;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (placeUnit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if(hit.collider.CompareTag("Ground"))
                    {
                        if(Gamemanager.instance.currentplayer == Gamemanager.instance.player1)
                        {
                            Unit go = Instantiate(unitRed, hit.point, Quaternion.identity);
                            go.health = this.health;
                            go.speed = this.speed;
                            go.defence = this.defence;
                            go.strength = this.strength;
                            placeUnit = false;
                            UI.SetActive(true);
                            Gamemanager.instance.SwitchCurrentPlayer();
                        }
                        else if (Gamemanager.instance.currentplayer == Gamemanager.instance.player2)
                        {
                            Unit go = Instantiate(unitBlue, hit.point, Quaternion.identity);
                            go.health = this.health;
                            go.speed = this.speed;
                            go.defence = this.defence;
                            go.strength = this.strength;
                            placeUnit = false;
                            UI.SetActive(true);
                            Gamemanager.instance.SwitchCurrentPlayer();
                        }

                    }
                }
            }
        }
    }

    private void OnEnable()
    {
        setDefaultValues();
    }

    public void ChangeHealth(int value)
    {
        if(value == 1)
        {
            health += value;
            healthTxt.text = health.ToString();
            Gamemanager.instance.currentplayer.points -= 3;
        }
        else
        {
            health += value;
            healthTxt.text = health.ToString();
            Gamemanager.instance.currentplayer.points += 3;
        }
    }

    public void ChangeSpeed(int value)
    {
        if (value == 1)
        {
            speed += value;
            speedTxt.text = speed.ToString();
            Gamemanager.instance.currentplayer.points -= 3;
        }
        else
        {
            speed += value;
            speedTxt.text = speed.ToString();
            Gamemanager.instance.currentplayer.points += 3;
        }
    }

    public void ChangeDefence(int value)
    {
        if (value == 1)
        {
            defence += value;
            defenceTxt.text = defence.ToString();
            Gamemanager.instance.currentplayer.points -= 2;
        }
        else
        {
            defence += value;
            defenceTxt.text = defence.ToString();
            Gamemanager.instance.currentplayer.points += 2;
        }
    }

    public void ChangeStrength(int value)
    {
        if (value == 1)
        {
            strength += value;
            strengthTxt.text = strength.ToString();
            Gamemanager.instance.currentplayer.points -= 2;
        }
        else
        {
            strength += value;
            strengthTxt.text = strength.ToString();
            Gamemanager.instance.currentplayer.points -= 2;
        }
    }

    public void HirePlayer()
    {
        int tHealth = health * 3;
        int tSpeed = speed * 3;
        int tDefence = defence * 2;
        int tStrength = strength * 2;

        Gamemanager.instance.currentplayer.points -= (tHealth + tSpeed + tDefence + tStrength);
        UI.SetActive(false);
    }

    void setDefaultValues()
    {
        health = 1;
        speed = 1;
        defence = 1;
        strength = 1;
        healthTxt.text = health.ToString();
        speedTxt.text = speed.ToString();
        defenceTxt.text = defence.ToString();
        strengthTxt.text = strength.ToString();
        points = Gamemanager.instance.currentplayer.points;
    }
}
