using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateTeam : MonoBehaviour
{
    public Camera mainCamera;
    public Text healthTxt, speedTxt, defenceTxt, strengthTxt, pointsTxt, playerNameTxt, playerPointsLeft;
    public Unit unitRed, unitBlue;
    public bool placeUnit = false;
    public GameObject UI;
    public Image avatarImg;

    Color lerpedColor = Color.white;

    int health;
    int speed;
    int defence;
    int strength;
    int points;

    private void Update()
    {
        lerpedColor = Color.Lerp(Color.red, Color.yellow, Mathf.PingPong(Time.time, 1));
        playerNameTxt.color = lerpedColor;
        if (placeUnit)
        {
            PlacePlayer();
        }
    }

    private void OnEnable()
    {
        setDefaultValues();
    }

    public void ChangeHealth(int value)
    {
        if(health >= 1)
        {
            if (value > 0)
            {
                points -= 2;
                health += value;
            }
            else if(health > 1)
            {
                points += 2;
                health += value;
            }
            updatePoints();

            healthTxt.text = health.ToString();
        }
    }

    public void ChangeSpeed(int value)
    {
        if(speed >= 1)
        {
            if (value > 0)
            {
                points -= 2;
                speed += value;
            }
            else if (speed > 1)
            {
                points += 2;
                speed += value;
            }
            updatePoints();
                
            speedTxt.text = speed.ToString();
        }
    }

    public void ChangeDefence(int value)
    {
        if(defence >= 1)
        {
            if (value > 0)
            {
                points -= 2;
                defence += value;
            }
            else if(defence > 1)
            {
                points += 2;
                defence += value;
            }
            updatePoints();

            defenceTxt.text = defence.ToString();
        }
    }

    public void ChangeStrength(int value)
    {
        if(strength >= 1)
        {
            if (value > 0)
            {
                points -= 2;
                strength += value;
            }
            else if(strength > 1)
            {
                points += 2;
                strength += value;
            }
            updatePoints();
            strengthTxt.text = strength.ToString();
        }
    }

    public void HirePlayer()
    {
        int tHealth = health * 3;
        int tSpeed = speed * 3;
        int tDefence = defence * 2;
        int tStrength = strength * 2;
        if(Gamemanager.instance.currentplayer.points < (tHealth + tSpeed + tDefence + tStrength))
        {
            Debug.Log("Player doesn't have enough points to hire this unit, you broke ass boi");
        }
        else if(Gamemanager.instance.currentplayer.points >= (tHealth + tSpeed + tDefence + tStrength))
        {
            Gamemanager.instance.currentplayer.points -= (tHealth + tSpeed + tDefence + tStrength);
            Debug.Log("curren players new points total = " + Gamemanager.instance.currentplayer.points);
            placeUnit = true;
            UI.SetActive(false);
        }
    }

    void setDefaultValues()
    {
        CheckPlayer();
        Debug.Log("its now this players turn" + Gamemanager.instance.currentplayer);
        health = 1;
        speed = 1;
        defence = 1;
        strength = 1;
        healthTxt.text = health.ToString();
        speedTxt.text = speed.ToString();
        defenceTxt.text = defence.ToString();
        strengthTxt.text = strength.ToString();
        playerPointsLeft.text = "Available points : " + Gamemanager.instance.currentplayer.points.ToString();
        playerNameTxt.text = Gamemanager.instance.currentplayer.name;
        points = Gamemanager.instance.currentplayer.points;
        avatarImg.sprite = Gamemanager.instance.currentplayer.characterImg;
        updatePoints();
    }

    void updatePoints()
    {
        int tTotalPoints = (health * 3 + speed * 3 + defence * 2 + strength * 2);
        pointsTxt.text = "Unit price " + tTotalPoints.ToString();
        
        Debug.Log(points);
    }

    public void PlacePlayer()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("moused clicked");
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("Hit the ground");
                    if (Gamemanager.instance.currentplayer == Gamemanager.instance.player1)
                    {
                        InstantiateUnit(unitRed, hit.point);
                    }
                    else if (Gamemanager.instance.currentplayer == Gamemanager.instance.player2)
                    {
                        InstantiateUnit(unitBlue, hit.point);
                    }
                    setDefaultValues();
                }
            }
        }
    }

    void InstantiateUnit(Unit unit, Vector3 hit)
    {
        Unit go = Instantiate(unit, hit, Quaternion.identity);
        go.health = this.health;
        go.speed = this.speed;
        go.defence = this.defence;
        go.strength = this.strength;

        placeUnit = false;
        UI.SetActive(true);
        Gamemanager.instance.SwitchCurrentPlayer();
    }

    public void CheckPlayer()
    {
        if (Gamemanager.instance.currentplayer.points <= 9)
            Gamemanager.instance.SwitchCurrentPlayer();
    }
}
