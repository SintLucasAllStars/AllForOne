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

    private void Awake()
    {
        instance = this;
        player1 = new Player(100, "Henk");
        player2 = new Player(100, "Roderik");
        currentplayer = player1;
        gameState = GameState.CreatTeam;
    }

    public bool CheckPoints()
    {
        if(player1.points <10 && player2.points < 10)
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
            gameState = GameState.Game;
        }
        else
        {
            if (currentplayer == player1)
                currentplayer = player2;
            else if (currentplayer == player2)
                currentplayer = player1;
        }
    }
}
