using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    using UnityEngine.EventSystems;

public class Unit2 : MonoBehaviour
{
    private int currentPlayer;
    private int startPoints;

    private Text[] player1UITexts;
    private Button[] Player1UIButtons; 
    private Text[] player2UITexts;
    private Button[] Player2UIButtons;

    private Text[] Player1Values;
    private Text[] Player2Values;

    private Text currentPlayerText;

    private Text strengthText;

    private int Strength;

    private bool clickingGuiElement;

        private void Awake() 
   { 
        player1UITexts= new Text[3];
        Player1UIButtons = new Button[8];
        player2UITexts= new Text[3];
        Player2UIButtons = new Button[8];
        
        Player1Values = new Text[2];
        Player2Values = new Text[2];
        
        startPoints = 100;
        currentPlayer = 1;
        Startup(currentPlayer);
    }

    private void Startup(int curPlayer)
    {
        if (curPlayer == 1)
        {
            Player1(startPoints);
        }
        else if (curPlayer == 2)
        {
            Player2(startPoints);
        }
    }

    private void Player1(int points)
    {
        DisableUIPlayer2();
        EnableUIPlayer1();
    }
    private void Player2(int points)
    {
        DisableUIPlayer1();
        EnableUIPlayer2();
    }
    
  
      private void DisableUIPlayer1()
    {
        for (int i = 0; i < player1UITexts.Length; i++)
        {
            Debug.Log("Work");
            player1UITexts[i] = GameObject.Find("player1UITexts" + i).GetComponent<Text>();
            player1UITexts[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < Player1UIButtons.Length; i++)
        {
            Player1UIButtons[i] = GameObject.Find("Player1UIButtons" + i).GetComponent<Button>();
            Player1UIButtons[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < Player1Values.Length; i++)
        {
            Player1Values[i] = GameObject.Find("player1Values" + i).GetComponent<Text>();
            Player1Values[i].gameObject.SetActive(false);
        }
      
    }
      private void DisableUIPlayer2()
      {
          for (int i = 0; i < player2UITexts.Length; i++)
          {
              Debug.Log("Work");
              player2UITexts[i] = GameObject.Find("player2UITexts" + i).GetComponent<Text>();
              player2UITexts[i].gameObject.SetActive(false);
          }
          for (int i = 0; i < Player2UIButtons.Length; i++)
          {
              Player2UIButtons[i] = GameObject.Find("Player2UIButtons" + i).GetComponent<Button>();
              Player2UIButtons[i].gameObject.SetActive(false);
          }
          for (int i = 0; i < Player2Values.Length; i++)
          {
              Player2Values[i] = GameObject.Find("player2Values" + i).GetComponent<Text>();
              Player2Values[i].gameObject.SetActive(false);
          }
      }

    private void EnableUIPlayer1()
    {
       currentPlayerText = GameObject.Find("CurrentPlayerText").GetComponent<Text>();
       currentPlayerText.text = "Player:" + currentPlayer;
        Debug.Log("Hello");

        Strength = 1;
        strengthText = GameObject.Find("player1Strength").GetComponent<Text>();
        strengthText.text = "Strength:" + Strength;
        
        for (int i = 0; i < player1UITexts.Length; i++)
        {
            player1UITexts[i] = GameObject.Find("player1UITexts" + i).GetComponent<Text>();
            player1UITexts[i].gameObject.SetActive(true);
            
           if (i % 2 == 0)
           {
               player1UITexts[i].GetComponentInChildren<Text>().text = "<";
           }
           else if (i % 2 != 0)
           {
               player1UITexts[i].GetComponentInChildren<Text>().text = ">";
           }
            
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
            Player1Values[i].text = "Points:" + startPoints;
        }
    }
    private void EnableUIPlayer2()
    {
        currentPlayerText = GameObject.Find("CurrentPlayerText").GetComponent<Text>();
        currentPlayerText.text = "Player:" + currentPlayer;
        Debug.Log("Hello");
        
        for (int i = 0; i < player1UITexts.Length; i++)
        {
            player2UITexts[i] = GameObject.Find("player2UITexts" + i).GetComponent<Text>();
            player2UITexts[i].gameObject.SetActive(true);
            
            if (i % 2 == 0)
            {
                player2UITexts[i].GetComponentInChildren<Text>().text = "<";
            }
            else if (i % 2 != 0)
            {
                player2UITexts[i].GetComponentInChildren<Text>().text = ">";
            }
            
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
            Player2Values[i].text = "Points:" + startPoints;
        }
    }

    private void Update()
    {
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject != null)
            {
                if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<CanvasRenderer>() != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        clickingGuiElement = true;
                        string name =  EventSystem.current.currentSelectedGameObject.name;
                        Debug.Log("ClickedButton");
                        Trigger_ButtonClick(name);
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

    
    public void Trigger_ButtonClick(string name)
    {
            if (name == "Player1UIButtons0" && Strength < 11 && Strength > 0)
            {
                Strength--;
                Debug.Log(Strength);
                strengthText= GameObject.Find("player1Strength").GetComponent<Text>();
                strengthText.text = "Strength:" + Strength;
                return;
            }
            
            if (name == "Player1UIButtons1" && Strength < 10 && Strength > -1)
            {
                Strength++;
                Debug.Log(Strength);
                strengthText= GameObject.Find("player1Strength").GetComponent<Text>();
                strengthText.text = "Strength:" + Strength;
                return;
            } 
    }
    }


