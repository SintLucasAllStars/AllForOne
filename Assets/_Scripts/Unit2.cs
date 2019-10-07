using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit2 : MonoBehaviour
{
    private int currentPlayer;
    private int startPoints;

    private Text[] Player1UI;

        private void Awake() 
   { 
        Player1UI = new Text[10];
        startPoints = 100;
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
        EnableUIPlayer1();
    }

    private void EnableUIPlayer1()
    {
        
    }

    private void Player2(int points)
    {
        EnableUIPlayer2();

    }
    private void EnableUIPlayer2()
    {
        
    }
}
