using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Player CurrentPlayer { get { return currentPlayer; } }

    public bool isSpawnMode = true;

    private List<Player> players = new List<Player>(0);
    private Player currentPlayer = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].enabled = false;
        }

        for (int i = 0; i < players.Count; i++)
        {
            if(players[i].index == 0)
            {
                players[i].enabled = true;
                currentPlayer = players[i];
            }
        }
    }

    public void NextTurn()
    {
        currentPlayer.enabled = false;

        ReadyCheck();

        if(currentPlayer.index + 1 >= players.Count)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(players[i].index == 0)
                    currentPlayer = players[i];
            }

            currentPlayer.enabled = true;
        }
        else
        {
            for (int i = 0; i < players.Count; i++)
            {
                if(players[i].index == currentPlayer.index + 1)
                {
                    currentPlayer = players[i];
                    currentPlayer.enabled = true;
                }
            }
        }
    }

    private void ReadyCheck()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].Ready)
                return;
        }

        isSpawnMode = false;
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }
}
