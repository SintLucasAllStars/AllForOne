using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    private PlayerControl playerController;
    private DisplayManager displayManager;

    private int turnTime;

    private List<Player> players;
    private GameState gameState;
    private WinState winState;

    private int currentPlayer;

    private void Start()
    {
        playerController = PlayerControl.Instance;
        displayManager = DisplayManager.Instance;

        winState.won = false;
        currentPlayer = 0;

        InitPlayers();
        StartCoroutine(Game());
    }

    private void InitPlayers()
    {
        players = new List<Player>();
        players.Add(new Player("player1"));
        players.Add(new Player("player2"));
    }

    public IEnumerator Game()
    {
        do
        {
            if (playerController.IsInTurn() == false) {
                bool isTurnOver = false;
                switch (gameState)
                {
                    case GameState.CreateStage:
                        if (PlayersHaveMoney()) {
                            if (players[currentPlayer].GetMoney() >= 8)
                            {
                                CreateWarrior();
                                isTurnOver = true;
                            }
                        }
                        else {
                            gameState = GameState.FightStage;
                            displayManager.ResetText();
                        }
                        break;
                    case GameState.FightStage:
                        displayManager.SetEventText(players[currentPlayer].GetName() + " can pick a warrior to fight with!");
                        playerController.SetTurn(players[currentPlayer], gameState);
                        isTurnOver = true;

                        for (int i = 0; i < players.Count; i++)
                        {
                            if (players[i].GetWarriors().Count <= 0)
                            {
                                players.Remove(players[i]);
                            }
                        }

                        if (players.Count <= 1)
                        {
                            winState.winner = players[0];
                            winState.won = true;
                        }

                        break;
                    default:
                        break;
                }
                if (isTurnOver == true) AddPlayerCount();
                yield return new WaitForEndOfFrame();   
            } else {
                yield return new WaitForEndOfFrame();
            }
        } while (winState.won == false);

        GameFin();
    }

    private void GameFin()
    {
        displayManager.ResetText();
        Debug.Log(winState.winner.GetName() + " won");
    }

    private void CreateWarrior()
    {
        playerController.SetTurn(players[currentPlayer], gameState);
        displayManager.DisplayPlayerSelect(players[currentPlayer]);
    }

    private bool PlayersHaveMoney()
    {
        if (players[currentPlayer].GetMoney() >= 8 || players[(currentPlayer + 1) % players.Count].GetMoney() >= 8)
        {
            return true;
        }

        return false;
    }

    private void AddPlayerCount()
    {
        int playercount = currentPlayer;
        playercount++;
        currentPlayer = (playercount % players.Count);
    }
}

struct WinState
{
    public Player winner;
    public bool won;
}

public enum GameState
{
    CreateStage, 
    FightStage
}