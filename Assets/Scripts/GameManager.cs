using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GamePhase { UnitBuying, UnitPlacing, UnitChoosing, UnitAction };
    public GamePhase gamePhase;

    [Header("References")]
    public GameObject playerPrefab;

    [Space]
    [Header("Game Stats")]
    [Tooltip("Starts counting from 0")] public int playerAmount = 1; // How many players are playing
    [SerializeField] int initialPoints = 100;

    [Space]
    [Header("Player Data")]
    public int[] playerPoints; // An array which keeps the player points for each player
    public bool[] canDoLoadoutPhase; // An array which keeps which players can still do the loadout phase
    public int currentTurnPlayer = 0; // Which player currently has their turn
    public List<GameObject>[] playerUnits; // Saves the gameobjects of each player's units

    // Start is called before the first frame update
    void Start()
    {
        // Calls InitialiseValues on start once
        InitialiseValues();
    }

    public List<GameObject> GetUnitAmount(int player)
    {
        return playerUnits[player];
    }

    public void DecreasePlayerPoints(int player, int value)
    {
        playerPoints[player] -= value;
    }

    // End the player's turn
    public void EndTurn()
    {
        // Cannot exceed the amount of players
        if (currentTurnPlayer < playerAmount)
        {
            currentTurnPlayer++;
        }
        else
        {
            currentTurnPlayer = 0;
        }

        // Different functionality for each phase
        switch (gamePhase)
        {
            case GamePhase.UnitBuying:
                // if can do loadout phase is false, skip that player's turn
                if (!canDoLoadoutPhase[currentTurnPlayer])
                {
                    CheckLoadoutPhase();
                }
                break;

            case GamePhase.UnitPlacing:
                // Set gamePhase back to unitBuying
                gamePhase = GamePhase.UnitBuying;

                if (!canDoLoadoutPhase[currentTurnPlayer])
                {
                    CheckLoadoutPhase();
                }
                break;

            case GamePhase.UnitChoosing:
                break;

            case GamePhase.UnitAction:
                break;

            default:
                break;
        }
    }

    void CheckLoadoutPhase()
    {
        // If all can do loadout phase are false, go to next phase
        if (!canDoLoadoutPhase.All(x => !x))
        {
            Debug.Log("Cannot do loadout phase.");
            EndTurn();
        }
        else
        {
            gamePhase = GamePhase.UnitChoosing;
        }
    }

    // Initialises all the game values
    // Called when a new game starts
    void InitialiseValues()
    {
        // Initialise arrays
        playerPoints = new int[playerAmount +1];
        canDoLoadoutPhase = new bool[playerAmount +1];
        playerUnits = new List<GameObject>[playerAmount +1];

        // Set values
        for (int i = 0; i < playerAmount +1; i++)
        {
            playerPoints[i] = initialPoints;
            canDoLoadoutPhase[i] = true;
            playerUnits[i] = new List<GameObject>();
        }
    }
}
