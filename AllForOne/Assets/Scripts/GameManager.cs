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

    private void Update()
    {
        
    }
}