using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events
    public delegate void GameStart();
    public event GameStart OnGameStart;

    public delegate void GameEnd();
    public event GameEnd OnGameEnd;
    #endregion

    #region Properties
    public Player CurrentPlayer     { get { return players[currentPlayer]; } }
    public Player Winner            { get { return winner; } }
    public bool SpawnMode           { get { return spawnMode; } }
    #endregion

    #region Fields
    public static GameManager instance = null;  //Singleton

    private List<Player> players = new List<Player>(0); //List of all players loaded in the scene.
    private Player winner = null;
    private int currentPlayer = 0;

    private bool spawnMode = true;
    #endregion

    #region Public Methods
    public void PlayerAdd(Player player)
    {
        players.Add(player);
    }

    public void PlayerRemove(Player player)
    {
        player.enabled = false;
        players.Remove(player);
        WinCheck();
    }

    //Pass the turn to the next player.
    public void NextTurn()
    {
        CurrentPlayer.enabled = false;

        //If all players are ready in spawn mode, start the game.
        if (spawnMode && ReadyCheck())
        {
            StartGame();
            return;
        }

        if (currentPlayer + 1 >= players.Count)
            currentPlayer = 0;
        else
            currentPlayer += 1;

        CurrentPlayer.enabled = true;
    }
    #endregion

    #region Methods
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DisablePlayers();
        currentPlayer = 0;
        CurrentPlayer.enabled = true;
    }

    //Checks whether all players are done placing units.
    private bool ReadyCheck()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].Ready)
                return false;
        }

        return true;
    }

    //Check whether there is a sole winner.
    private void WinCheck()
    {
        if (players.Count == 1)
        {
            winner = players[0];
            EndGame();
        }
    }

    //Disable all players.
    private void DisablePlayers()
    {
        foreach (Player player in players)
        {
            player.enabled = false;
        }
    }
    
    //Handles starting the game.
    private void StartGame()
    {
        WinCheck();
        spawnMode = false;
        currentPlayer = 0;
        CurrentPlayer.enabled = true;

        if (OnGameStart != null)
            OnGameStart();
    }

    //Handles ending the game.
    private void EndGame()
    {
        DisablePlayers();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (OnGameEnd != null)
            OnGameEnd();
    }
    #endregion
}
