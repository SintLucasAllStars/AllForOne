using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : MonoBehaviour
{
    public enum GameState { CreatTeam, Game}
    public GameState gameState;
    public static Gamemanager instance;
    public Player currentplayer;
    public Player player1;
    public Player player2;
    public GameObject canvas;
    public GameObject topview;

    private void Awake()
    {
        instance = this;
        player1 = new Player(11, "Henk", "Red");
        player2 = new Player(11, "Roderik", "Blue");
        currentplayer = player1;
    }

    public bool CheckPoints()
    {
        if(player1.points <=9 && player2.points <=9)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwitchCurrentPlayer()
    {
        if (CheckPoints())
        {
            Debug.Log("Both players have points below 10");
            currentplayer = player1;
            canvas.SetActive(false);
            topview.SetActive(true);
        }

        else
        {
            SwitchPlayer();
        }
    }

    public void CheckForUnits()
    {
        if(GameObject.FindGameObjectsWithTag("Red") == null)
        {
            Debug.Log("Blue team wins");
        }
        if (GameObject.FindGameObjectsWithTag("Blue") == null)
        {
            Debug.Log("Red team wins");
        }

    }

    public void SwitchPlayer()
    {
        if (currentplayer == player1)
            currentplayer = player2;
        else if (currentplayer == player2)
            currentplayer = player1;
    }

    public void TopViewTurnOn()
    {
        if (topview.activeInHierarchy == true)
            topview.SetActive(false);
        else if (topview.activeInHierarchy == false)
            topview.SetActive(true);
    }
}
