using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Player> players;
    private GameState gameState;
    private WinState winState;

    private int currentPlayer;
    private PlayerControl cameraControll;

    private void Start()
    {
        cameraControll = PlayerControl.Instance;
        winState.won = false;
        currentPlayer = 0;

        InitPlayers();
        Game();
    }

    public void Game()
    {
        do
        {
            switch (gameState)
            {
                case GameState.CreateStage:



                    break;
                case GameState.FightStage:
                    break;
                default:
                    break;
            }
        } while (winState.won);
    }

    private void InitPlayers()
    {
        players = new List<Player>();
        players.Add(new Player("player1"));
        players.Add(new Player("player2"));
    }
}

struct WinState
{
    public Player winner;
    public bool won;
}

enum GameState
{
    CreateStage, 
    FightStage
}