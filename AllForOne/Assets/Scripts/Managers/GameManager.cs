using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    

    private void Awake()
    {
        gameManager = this;
    }


    /// <summary>
    /// The state the game is currently in. MenuState is the main menu that anyone can activate. Buystate is when the players are buying their units. 
    /// Overlook state is when one player is choosing their unit to play. PlayState is when a player controls one of the units.
    /// </summary>
    public enum GameState {MenuState, BuyState, OverlookState, PlayState}
    public static GameState currentGameState;

    //Unit and Team Information
    public Unit currentTurn;
    public BaseTeam[] team;
    private int selectedTeam = 0;

    //Turn information
    public TMP_Text gameInfoText;

    //Buy Menu
    public GameObject doneBuyingButton;

    //Turn Timers
    private readonly float maxTime = 10;
    private float turnTimeRemaining;

    public TMP_Text timeText;
    private bool turnTimer;

    //Combat Timers
    private bool stopTimer = false;
    private bool delayTimer = false;
    private float delayTimerTime;

    /// <summary>
    /// Call this when the gamestate needs a switch.
    /// </summary>
    /// <param name="gameState"></param>
    public void SwitchGameState(GameState gameState)
    {
        currentGameState = gameState;
        switch (gameState)
        {
            case GameState.BuyState:
                CameraManager.cameraManager.MoveCamera(CameraManager.cameraManager.overviewCameraPosition);
                gameInfoText.text = "Team " + (selectedTeam + 1) + "'s turn. Click on a surface to buy a unit. If you have less then 10 points you can't buy a unit.";

                doneBuyingButton.SetActive(true);
                break;

            case GameState.OverlookState:
                CameraManager.cameraManager.MoveCamera(CameraManager.cameraManager.overviewCameraPosition);
                gameInfoText.text = "Team " + (selectedTeam + 1) + "'s turn. Click on one of your units to play him.";

                doneBuyingButton.SetActive(false);
                break;

            case GameState.PlayState:
                gameInfoText.text = "";
                break;
        }
    }

    private void Start()
    {
        SwitchGameState(GameState.BuyState);
        team[selectedTeam].StartTurn();
    }

    private void FixedUpdate()
    {
        //The timer used to calculate how long the turn lasts.
        if ((turnTimer) && (!stopTimer))
        {
            turnTimeRemaining -= Time.deltaTime;
            if ((turnTimeRemaining < 0) && (!currentTurn.isAttacking))
            {
                team[selectedTeam].EndTurn();
            }
            int intTime = Mathf.RoundToInt(turnTimeRemaining);

            timeText.text = "Time Left: " + intTime;
        }

        //The timer used when an action is taken in a turn. Like attacking or combat.
        if (delayTimer)
        {
            delayTimerTime -= Time.deltaTime;
            if (delayTimerTime < 0)
            {
                gameInfoText.text = "";
                currentTurn.EndAttack();
                stopTimer = false;
                delayTimer = false;
            }
        }
    }

    /// <summary>
    /// Call this when the button of ending the turn is pressed.
    /// </summary>
    public void ButtonEndEarly()
    {
        team[selectedTeam].EndTurn();
    }


    public void ButtonDoneBuying()
    {
        team[selectedTeam].doneBuying = true;
        team[selectedTeam].EndTurn();
    }

    /// <summary>
    /// Call this when one unit attacks. Delays the turnTimer.
    /// </summary>
    public void UnitAttacking(float waitTime)
    {
        delayTimerTime = waitTime;
        delayTimer = true;
        stopTimer = false;
        gameInfoText.text = "Attacking!";
    }

    /// <summary>
    /// Call this when one unit starts or ends combat. Stops or Continues the turnTimer.
    /// </summary>
    public void CombatTimer(bool starts)
    {
        stopTimer = starts;
    }

    

    /// <summary>
    /// A unit gets selected in the OverlookState. selectedUnit is the unit that will start his turn.
    /// </summary>
    public void StartTurn(Unit selectedUnit)
    {
        turnTimer = true;
        turnTimeRemaining = maxTime;
    }

    /// <summary>
    /// Call this when someone's turn ended.
    /// </summary>
    public void StopTurn()
    {
        turnTimer = false;
        timeText.text = " ";

        if (currentGameState == GameState.PlayState)
        {
            currentTurn.StopSelectedEarly();
        }
        CameraManager.cameraManager.MoveCamera(CameraManager.cameraManager.overviewCameraPosition);

        NextTeamTurn();
    }
    
    /// <summary>
    /// Call this when a unit's turn starts.
    /// </summary>
    public void ChosenUnit(Unit unit)
    {
        currentTurn = unit;
    }

    /// <summary>
    /// When one player ends it's turn.
    /// </summary>
    private void NextTeamTurn()
    {
        if (selectedTeam == team.Length - 1)
        {
            selectedTeam = 0;
        }
        else
        {
            selectedTeam = selectedTeam + 1;
        }

        if (((team[0].unitPoints < 10) && (team[1].unitPoints < 10)) || ((team[0].doneBuying) && (team[1].doneBuying)))
        {
            SwitchGameState(GameState.OverlookState);
        }
        else
        {
            if (team[selectedTeam].doneBuying)
            {
                NextTeamTurn();
                return;
            }
            else
            {
                SwitchGameState(GameState.BuyState);
            }
        }

        team[selectedTeam].StartTurn();
    }

    public IEnumerator StartOver(float waitTime)
    {
        gameInfoText.text = "Team " + (selectedTeam + 1) + " has lost! You will be redirected to the main screen shortly";
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(0);
    }
}