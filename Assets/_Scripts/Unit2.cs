using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
    using UnityEngine.EventSystems;

public class Unit2 : MonoBehaviour
{
    public static int currentPlayer;
    private int Player1PointsLeft, Player1PointsSpent, Player2PointsLeft, Player2PointsSpent;

    private Text[] player1UITexts;
    private Button[] Player1UIButtons;
    private Text[] player2UITexts;
    private Button[] Player2UIButtons;

    private Text[] Player1Values;
    private Text[] Player2Values;

    private Text currentPlayerText;

    private Text strengthText, healthText, speedText, defenceText;

    public static int Strength, Health, Speed, Defence;

    private bool clickingGuiElement;

    private int costPlayer1, costPlayer2;

    private bool Player1Done = false, Player2Done = false;

    private void Awake()
    {
        player1UITexts = new Text[3];
        Player1UIButtons = new Button[8];
        player2UITexts = new Text[3];
        Player2UIButtons = new Button[8];

        Player1Values = new Text[2];
        Player2Values = new Text[2];

        Player1PointsLeft = 100;
        Player1PointsSpent = 0;
        Player2PointsLeft = 100;
        Player2PointsSpent = 0;
        currentPlayer = 1;
        Startup(currentPlayer);
    }

    private void Startup(int curPlayer)
    {
        if (curPlayer == 1)
        {
            Player1(Player1PointsLeft);
        }
        else if (curPlayer == 2)
        {
            Player2(Player1PointsLeft);
        }
    }

    public void Player1(int points)
    {
        DisableUIPlayer2();
        EnableUIPlayer1();
    }

    public void Player2(int points)
    {
        DisableUIPlayer1();
        EnableUIPlayer2();
    }


    private void DisableUIPlayer1()
    {
        strengthText = GameObject.Find("player1Strength").GetComponent<Text>();
        strengthText.gameObject.tag = "Inactive";
        strengthText.gameObject.SetActive(false);
        healthText = GameObject.Find("player1Health").GetComponent<Text>();
        healthText.gameObject.tag = "Inactive";
        healthText.gameObject.SetActive(false);
        speedText = GameObject.Find("player1Speed").GetComponent<Text>();
        speedText.gameObject.tag = "Inactive";
        speedText.gameObject.SetActive(false);
        defenceText = GameObject.Find("player1Defence").GetComponent<Text>();
        defenceText.gameObject.tag = "Inactive";
        defenceText.gameObject.SetActive(false);
     
        for (int i = 0; i < player1UITexts.Length; i++)
        {
            Debug.Log("Work");
            player1UITexts[i] = GameObject.Find("player1UITexts" + i).GetComponent<Text>();
            player1UITexts[i].gameObject.tag = "Inactive";
            player1UITexts[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Player1UIButtons.Length; i++)
        {
            Player1UIButtons[i] = GameObject.Find("Player1UIButtons" + i).GetComponent<Button>();
            Player1UIButtons[i].gameObject.tag = "Inactive";
            Player1UIButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Player1Values.Length; i++)
        {
            Player1Values[i] = GameObject.Find("player1Values" + i).GetComponent<Text>();
            Player1Values[i].gameObject.tag = "Inactive";
            Player1Values[i].gameObject.SetActive(false);
        }

    }

    private void DisableUIPlayer2()
    {
        strengthText = GameObject.Find("player2Strength").GetComponent<Text>();
        strengthText.gameObject.tag = "Inactive";
        strengthText.gameObject.SetActive(false);
        healthText = GameObject.Find("player2Health").GetComponent<Text>();
        healthText.gameObject.tag = "Inactive";
        healthText.gameObject.SetActive(false);
        speedText = GameObject.Find("player2Speed").GetComponent<Text>();
        speedText.gameObject.tag = "Inactive";
        speedText.gameObject.SetActive(false);
        defenceText = GameObject.Find("player2Defence").GetComponent<Text>();
        defenceText.gameObject.tag = "Inactive";
        defenceText.gameObject.SetActive(false);
        for (int i = 0; i < player2UITexts.Length; i++)
        {
            Debug.Log("Work");
            player2UITexts[i] = GameObject.Find("player2UITexts" + i).GetComponent<Text>();
            player2UITexts[i].gameObject.tag = "Inactive";
            player2UITexts[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Player2UIButtons.Length; i++)
        {
            Player2UIButtons[i] = GameObject.Find("Player2UIButtons" + i).GetComponent<Button>();
            Player2UIButtons[i].gameObject.tag = "Inactive";
            Player2UIButtons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Player2Values.Length; i++)
        {
            Player2Values[i] = GameObject.Find("player2Values" + i).GetComponent<Text>();
            Player2Values[i].gameObject.tag = "Inactive";
            Player2Values[i].gameObject.SetActive(false);
        }
    }

    private void EnableUIPlayer1()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Inactive");
 
        foreach(GameObject go in gameObjectArray)
        {
            go.SetActive (true);
        }

        currentPlayerText = GameObject.Find("CurrentPlayerText").GetComponent<Text>();
        currentPlayerText.text = "Player:" + currentPlayer;
        Debug.Log("Hello");

        Strength = 1;
        Health = 1;
        Speed = 1;
        Defence = 1;

        costPlayer1 = 8;
        
        strengthText = GameObject.Find("player1Strength").GetComponent<Text>();
        strengthText.text = "Strength:" + Strength;
        healthText = GameObject.Find("player1Health").GetComponent<Text>();
        healthText.text = "Health:" + Health;
        speedText = GameObject.Find("player1Speed").GetComponent<Text>();
        speedText.text = "Speed:" + Speed;
        defenceText = GameObject.Find("player1Defence").GetComponent<Text>();
        defenceText.text = "Defence:" + Defence;

        for (int i = 0; i < player1UITexts.Length; i++)
        {
            player1UITexts[i] = GameObject.Find("player1UITexts" + i).GetComponent<Text>();
            player1UITexts[i].gameObject.SetActive(true);

            if (player1UITexts[i] == player1UITexts[0])
                player1UITexts[i].GetComponentInChildren<Text>().text = "Cost:" + costPlayer1;

        }

        for (int i = 0; i < Player1UIButtons.Length; i++)
        {
            Player1UIButtons[i] = GameObject.Find("Player1UIButtons" + i).GetComponent<Button>();
            Player1UIButtons[i].gameObject.SetActive(true);
            if (i % 2 == 0)
            {
                Player1UIButtons[i].GetComponentInChildren<Text>().text = "<";
            }
            else if (i % 2 != 0)
            {
                Player1UIButtons[i].GetComponentInChildren<Text>().text = ">";
            }
        }

        for (int i = 0; i < Player1Values.Length; i++)
        {
            Debug.Log("Hello2");
            Player1Values[i] = GameObject.Find("player1Values" + i).GetComponent<Text>();
            Player1Values[i].gameObject.SetActive(true);
            if (Player1Values[i] == Player1Values[0])
            Player1Values[i].text = "Points:" + Player1PointsLeft;
            if (Player1Values[i] == Player1Values[1])
            Player1Values[i].text = "Spent:" + Player1PointsSpent;
        }
    }

    private void EnableUIPlayer2()
    {
        EnableStuff();
        currentPlayerText = GameObject.Find("CurrentPlayerText").GetComponent<Text>();
        currentPlayerText.text = "Player:" + currentPlayer;
        
        Strength = 1;
        Health = 1;
        Speed = 1;
        Defence = 1;

        costPlayer2 = 8;
       
        strengthText = GameObject.Find("player2Strength").GetComponent<Text>();
        strengthText.gameObject.SetActive(true);
        strengthText.text = "Strength:" + Strength;
        healthText = GameObject.Find("player2Health").GetComponent<Text>();
        healthText.text = "Health:" + Health;
        speedText = GameObject.Find("player2Speed").GetComponent<Text>();
        speedText.text = "Speed:" + Speed;
        defenceText = GameObject.Find("player2Defence").GetComponent<Text>();
        defenceText.text = "Defence:" + Defence;
      
        Debug.Log("Hello");
        for (int i = 0; i < player2UITexts.Length; i++)
        {
            player2UITexts[i] = GameObject.Find("player2UITexts" + i).GetComponent<Text>();
            player2UITexts[i].gameObject.SetActive(true);

            if (player2UITexts[i] == player2UITexts[0])
                player2UITexts[i].GetComponentInChildren<Text>().text = "Cost:" + costPlayer2;

        }

        for (int i = 0; i < Player2UIButtons.Length; i++)
        {
            Player2UIButtons[i] = GameObject.Find("Player2UIButtons" + i).GetComponent<Button>();
            Player2UIButtons[i].gameObject.SetActive(true);
            if (i % 2 == 0)
            {
                Player2UIButtons[i].GetComponentInChildren<Text>().text = "<";
            }
            else if (i % 2 != 0)
            {
                Player2UIButtons[i].GetComponentInChildren<Text>().text = ">";
            }
        }

        for (int i = 0; i < Player1Values.Length; i++)
        {
            Debug.Log("Hello2");
            Player2Values[i] = GameObject.Find("player2Values" + i).GetComponent<Text>();
            Player2Values[i].gameObject.SetActive(true);
            if (Player2Values[i] == Player2Values[0])
            Player2Values[0].text = "Points:" + Player2PointsLeft;
            if (Player2Values[i] == Player2Values[1])
            Player2Values[1].text = "Spent:" + Player2PointsSpent;
        }
    }

    private void EnableStuff()
    {
        GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag ("Inactive");
 
        foreach(GameObject go in gameObjectArray)
        {
            go.SetActive (true);
        }}

    private void Update()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
            {
                if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject
                        .GetComponent<CanvasRenderer>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        clickingGuiElement = true;
                        string name = EventSystem.current.currentSelectedGameObject.name;
                        Debug.Log("ClickedButton");
                        Trigger_ButtonClick(name, currentPlayer);
                    }
                }
                else
                {
                    clickingGuiElement = false;
                }
            }
            else
            {
                clickingGuiElement = false;
            }
        }
        else
        {
            clickingGuiElement = false;
        }
    }


    public void Trigger_ButtonClick(string name, int player)
    {
        switch (player)
        {
            case 1:
                run1(name);
                break;
            case 2:
                run2(name);
                break;

        }


    }

    private void run1(string name)
    {
        if (name == "ReadyButton")
        {
            Player1Done = true;
        }
        if (name == "PlaceButton")
        {
            UnitPlacement1.Save();
            Player2(Player2PointsLeft);
        }
        
        if (name == "Player1UIButtons0" || name == "Player1UIButtons1")
        {
            if (name == "Player1UIButtons0" && Strength < 11 && Strength > 0)
            {
                Strength--;
                costPlayer1 -= 2;
                Cost();
                Debug.Log(Strength);
                strengthText = GameObject.Find("player1Strength").GetComponent<Text>();
                strengthText.text = "Strength:" + Strength;
                return;
            }

            if (name == "Player1UIButtons1" && Strength < 10 && Strength > -1)
            {
                Strength++;
                costPlayer1 += 2;
                Cost();
                Debug.Log(Strength);
                strengthText = GameObject.Find("player1Strength").GetComponent<Text>();
                strengthText.text = "Strength:" + Strength;
                return;
            }
        }
        else if (name == "Player1UIButtons2" || name == "Player1UIButtons3")
        {
            if (name == "Player1UIButtons2" && Health < 11 && Health > 0)
            {
                Health--;
                costPlayer1 -= 2;
                Cost();
                Debug.Log(Health);
                healthText = GameObject.Find("player1Health").GetComponent<Text>();
                healthText.text = "Health:" + Health;
                return;
            }

            if (name == "Player1UIButtons3" && Health < 10 && Health > -1)
            {
                Health++;
                costPlayer1 += 2;
                Cost();
                Debug.Log(Health);
                healthText = GameObject.Find("player1Health").GetComponent<Text>();
                healthText.text = "Health:" + Health;
                return;
            }
        }
        else if (name == "Player1UIButtons4" || name == "Player1UIButtons5")
        {
            if (name == "Player1UIButtons4" && Speed < 11 && Speed > 0)
            {
                Speed--;
                costPlayer1 -= 2;
                Cost();
                Debug.Log(Speed);
                speedText = GameObject.Find("player1Speed").GetComponent<Text>();
                speedText.text = "Speed:" + Speed;
                return;
            }

            if (name == "Player1UIButtons5" && Speed < 10 && Speed > -1)
            {
                Speed++;
                costPlayer1 += 2;
                Cost();
                Debug.Log(Speed);
                speedText = GameObject.Find("player1Speed").GetComponent<Text>();
                speedText.text = "Speed:" + Speed;
                return;
            }
        }
        else if (name == "Player1UIButtons6" || name == "Player1UIButtons7")
        {
            if (name == "Player1UIButtons6" && Defence < 11 && Defence > 0)
            {
                Defence--;
                costPlayer1 -= 2;
                Cost();
                Debug.Log(Defence);
                defenceText = GameObject.Find("player1Defence").GetComponent<Text>();
                defenceText.text = "Defence:" + Defence;
                return;
            }

            if (name == "Player1UIButtons7" && Defence < 10 && Defence > -1)
            {
                Defence++;
                costPlayer1 += 2;
                Cost();
                Debug.Log(Defence);
                defenceText = GameObject.Find("player1Defence").GetComponent<Text>();
                defenceText.text = "Defence:" + Defence;
                return;
            }
        }
    }

    private void run2(string name)
    {
        
        if (name == "ReadyButton")
        {
            Player2Done = true;
        }
        if (name == "PlaceButton")
        {
            UnitPlacement2.Save();
            Player1(Player1PointsLeft);
        }
        
        if (name == "Player2UIButtons0" || name == "Player2UIButtons1")
        {
            if (name == "Player2UIButtons0" && Strength < 11 && Strength > 0)
            {
                Strength--;
                costPlayer2 -= 2;
                Cost();
                Debug.Log(Strength);
                strengthText = GameObject.Find("player2Strength").GetComponent<Text>();
                strengthText.text = "Strength:" + Strength;
                return;
            }

            if (name == "Player2UIButtons1" && Strength < 10 && Strength > -1)
            {
                Strength++;
                costPlayer2 += 2;
                Cost();
                Debug.Log(Strength);
                strengthText = GameObject.Find("player2Strength").GetComponent<Text>();
                strengthText.text = "Strength:" + Strength;
                return;
            }
        }
        else if (name == "Player2UIButtons2" || name == "Player2UIButtons3")
        {
            if (name == "Player2UIButtons2" && Health < 11 && Health > 0)
            {
                Health--;
                costPlayer2 -= 2;
                Cost();
                Debug.Log(Health);
                healthText = GameObject.Find("player2Health").GetComponent<Text>();
                healthText.text = "Health:" + Health;
                return;
            }

            if (name == "Player2UIButtons3" && Health < 10 && Health > -1)
            {
                Health++;
                costPlayer2 += 2;
                Cost();
                Debug.Log(Health);
                healthText = GameObject.Find("player2Health").GetComponent<Text>();
                healthText.text = "Health:" + Health;
                return;
            }
        }
        else if (name == "Player2UIButtons4" || name == "Player2UIButtons5")
        {
            if (name == "Player2UIButtons4" && Speed < 11 && Speed > 0)
            {
                Speed--;
                costPlayer2 -= 2;
                Cost();
                Debug.Log(Speed);
                speedText = GameObject.Find("player2Speed").GetComponent<Text>();
                speedText.text = "Speed:" + Speed;
                return;
            }

            if (name == "Player2UIButtons5" && Speed < 10 && Speed > -1)
            {
                Speed++;
                costPlayer2 += 2;
                Cost();
                Debug.Log(Speed);
                speedText = GameObject.Find("player2Speed").GetComponent<Text>();
                speedText.text = "Speed:" + Speed;
                return;
            }
        }
        else if (name == "Player2UIButtons6" || name == "Player2UIButtons7")
        {
            if (name == "Player2UIButtons6" && Defence < 11 && Defence > 0)
            {
                Defence--;
                costPlayer2 -= 2;
                Cost();
                Debug.Log(Defence);
                defenceText = GameObject.Find("player2Defence").GetComponent<Text>();
                defenceText.text = "Defence:" + Defence;
                return;
            }

            if (name == "Player2UIButtons7" && Defence < 10 && Defence > -1)
            {
                Defence++;
                costPlayer2 += 2;
                Cost();
                Debug.Log(Defence);
                defenceText = GameObject.Find("player2Defence").GetComponent<Text>();
                defenceText.text = "Defence:" + Defence;
                return;
            }
        }
    }

    public void Cost()
    {
        if (currentPlayer == 1)
        {
            for (int i = 0; i < player1UITexts.Length; i++)
            {
                player1UITexts[i] = GameObject.Find("player1UITexts" + i).GetComponent<Text>();
                player1UITexts[i].gameObject.SetActive(true);

                if (player1UITexts[i] == player1UITexts[0])
                    player1UITexts[i].GetComponentInChildren<Text>().text = "Cost:" + costPlayer1;

            }
        }
        else if (currentPlayer == 2)
        {
            for (int i = 0; i < player2UITexts.Length; i++)
            {
                player2UITexts[i] = GameObject.Find("player2UITexts" + i).GetComponent<Text>();
                player2UITexts[i].gameObject.SetActive(true);

                if (player2UITexts[i] == player2UITexts[0])
                    player2UITexts[i].GetComponentInChildren<Text>().text = "Cost:" + costPlayer2;

            }
        }
       
    }
}


