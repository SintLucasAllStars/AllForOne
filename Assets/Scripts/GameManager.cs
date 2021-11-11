using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cinemachine;

public class GameManager : Singleton<GameManager>
{
    public enum GamePhase { UnitBuying, UnitPlacing, UnitChoosing, UnitAction };
    public GamePhase gamePhase;

    [Header("References")]
    public GameObject playerPrefab;
    public Timer timer;
    public GameObject[] vCams;

    [Space]
    [Header("Game Stats")]
    [Tooltip("Starts counting from 0")] public int playerAmount = 1; // How many players are playing
    [SerializeField] int initialPoints = 100;
    public float cameraTimer = 3;
    public float playerTimer = 10;
    [SerializeField] Color[] possibleColors;

    [Space]
    [Header("Player Data")]
    public int[] playerPoints; // An array which keeps the player points for each player
    public bool[] canDoLoadoutPhase; // An array which keeps which players can still do the loadout phase
    public int currentTurnPlayer = 0; // Which player currently has their turn
    public List<GameObject>[] playerUnits; // Saves the gameobjects of each player's units
    public bool playerActionActive;
    UnitMovement currentMovingUnit;

    // Start is called before the first frame update
    void Start()
    {
        // Calls InitialiseValues on start once
        InitialiseValues();

        // Make a new Timer
        timer = new Timer();
    }

    void Update()
    {
        if (gamePhase == GamePhase.UnitAction)
        {
            // If the movement timer is over
            if (timer.Tick() <= 0)
            {
                // Stop Timer
                timer.StopTimer();

                // Player can no longer move
                playerActionActive = false;
                currentMovingUnit.canMove = false;

                // Set camera back
                ChangeCamera(0);

                timer.ResetTimer();

                EndTurn();
            }
            
        }
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
                // Reset timer value
                UIManager.Instance.UpdatePlayerTimerText(timer.GetCurrentDuration());
                // Set player turn text to current player
                UIManager.Instance.ChangePlayerText(currentTurnPlayer);

                gamePhase = GamePhase.UnitChoosing;
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

    public void StartPlayerSequence(GameObject unit)
    {
        // If current turn player owns the selected unit
        if (unit.GetComponent<UnitMovement>().unitStats.GetOwner() == currentTurnPlayer)
        {
            StartCoroutine(PlayerMoveSequence(unit, cameraTimer, playerTimer));
        }
        else
        {
            Debug.Log("Player does not own this unit!");
        }
    }

    IEnumerator PlayerMoveSequence(GameObject unit, float cameraTimer, float playerTimer)
    {
        // Set camera and follow target
        SetCameraFollowTarget(1, unit);
        yield return StartCoroutine(ChangeCameraSequence(1, cameraTimer));

        // Start Timer
        timer.StartTimer(playerTimer);

        // Player can move
        gamePhase = GamePhase.UnitAction;
        currentMovingUnit = unit.GetComponent<UnitMovement>();
        currentMovingUnit.canMove = true;
        playerActionActive = true;
    }

    IEnumerator ChangeCameraSequence(int cam, float cameraTimer)
    {
        ChangeCamera(cam);

        yield return new WaitForSeconds(cameraTimer);
    }

    void ChangeCamera(int camToChangeTo)
    {
        for (int i = 0; i < vCams.Length; i++)
        {
            vCams[i].SetActive(false);
        }

        vCams[camToChangeTo].SetActive(true);
    }

    void SetCameraFollowTarget(int camToChange, GameObject followTarget)
    {
        CinemachineFreeLook cam = vCams[camToChange].GetComponent<CinemachineFreeLook>();
        cam.Follow = followTarget.transform;
        cam.LookAt = followTarget.transform;
    }

    public GameObject CreateUnit(Vector3 location, int owner, int hea, int str, int spe, int def)
    {
        GameObject unit = Instantiate(playerPrefab, location, Quaternion.identity);
        UnitMovement unitMovement = unit.GetComponent<UnitMovement>();

        // Add stats to the unit
        unitMovement.unitStats = new Unit(owner, hea, str, spe, def);

        // Depending on which player owns this unit, change color
        Color unitColor = possibleColors[currentTurnPlayer];
        unitMovement.ChangeColor(unitColor);

        return unit;
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
