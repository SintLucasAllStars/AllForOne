using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager instance { get; private set; }

    [Header("Game Stats")]
    [Tooltip("Starts counting from 0")] public int playerAmount = 1; // How many players are playing
    [SerializeField] int initialPoints = 100;

    [Space]
    [Header("Player Data")]
    public int[] playerPoints; // An array which keeps the player points for each player
    public bool[] canDoLoadoutPhase; // An array which keeps which players can still do the loadout phase
    public int currentTurnPlayer = 0; // Which player currently has their turn
    public List<GameObject>[] playerUnits; // Saves the gameobjects of each player's units

    private void Awake()
    {
        // Singleton
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        }
    }
}
