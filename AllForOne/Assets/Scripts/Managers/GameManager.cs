using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// The state the game is currently in. MenuState is the main menu that anyone can activate. Buystate is when the players are buying their units. 
    /// Overlook state is when one player is choosing their unit to play. PlayState is when a player controls one of the units.
    /// </summary>
    public enum GameState {MenuState, BuyState, OverlookState, PlayState}
    public static GameState currentGameState;

    private BaseTeam[] team;
    private int selectedTeam = 0;

    private void Update()
    {
        
    }

    /// <summary>
    /// When one player ends it's turn.
    /// </summary>
    public void NextTeamTurn()
    {
        if (selectedTeam == team.Length - 1)
        {
            selectedTeam = 0;
        }
        else
        {
            selectedTeam = selectedTeam + 1;
        }

        switch (currentGameState)
        {
            case GameState.BuyState:
                team[selectedTeam].BuyUnit();
                break;
            case GameState.OverlookState:
                team[selectedTeam].SelectUnit();
                break;
        }
    }

    /// <summary>
    /// A unit gets selected in the OverlookState. selectedUnit is the unit that will start his turn.
    /// </summary>
    public void StartSelectedTurn(Unit selectedUnit)
    {
        
    }

    private void SwitchGameState()
    {
        switch (currentGameState)
        {
            case GameState.MenuState:

                break;
            case GameState.BuyState:

                break;
            case GameState.OverlookState:

                break;
            case GameState.PlayState:

                break;
        }
    }
}