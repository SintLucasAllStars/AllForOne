using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Invector;

public class GameManager : MonoBehaviour
{
    private Player one = new Player(1);
    private Player two = new Player(2);
    private Player currentPlayer;
    private bool firstPlayerTurn = true;

    private Color blueText = new Vector4(0.5f, 0.5f, 1f, 1f);

    public float gameplayTime = 10f;
    private float timer;

    public GameObject unitStoreUI;
    public GameObject CurrentPointsUI;
    public Text currentPlayerText;
    public Text gameplayTimerText;
    public GameObject selectUnitText;

    public UnitCreator unitCreator;
    public UnitPlacement unitPlacement;

    private bool unitSelectionScreen = false;
    private bool gameplayActive = false;
    private UnitBehaviour activeUnit;

    public Camera unitCam;
    public vThirdPersonCamera tpCamera;

    void Start()
    {
        timer = gameplayTime;
        currentPlayer = one;
    }

    void Update()
    {
        UpdateTimer();
    }

    private void StartGame()
    {
        // Deactivate unit creator UI and functionality.
        unitStoreUI.SetActive(false);
        CurrentPointsUI.SetActive(false);
        unitCreator.enabled = false;
        unitPlacement.enabled = false;
        SetUnitSelectionScreenActive();
    }

    private void SetUnitSelectionScreenActive()
    {
        unitSelectionScreen = true;
        selectUnitText.SetActive(true);

        if (activeUnit != null)
        {
            activeUnit.LockMovement();
            tpCamera.target = null;
            unitCam.enabled = false;
            activeUnit = null;
        }
    }

    public void SetGameplayActive(UnitBehaviour unit)
    {
        activeUnit = unit;
        gameplayActive = true;
        unitSelectionScreen = false;
        selectUnitText.SetActive(false);
        tpCamera.SetMainTarget(activeUnit.transform);
        unitCam.enabled = true;
    }



    public void UpdateCurrentPlayerText()
    {
        currentPlayerText.text = "PLAYER " + currentPlayer.GetPlayerNumber();
        if (currentPlayer == one)
        {
            currentPlayerText.color = Color.red;
        }
        else if (currentPlayer == two)
        {
            currentPlayerText.color = blueText;
        }
    }

    public void SetCurrentPlayer(Player current)
    {
        currentPlayer = current;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void FlipPlayer()
    {
        firstPlayerTurn = !firstPlayerTurn;

        // Flip between players one and two.
        if (!firstPlayerTurn)
        {
            currentPlayer = two;
        }
        else if (firstPlayerTurn)
        {
            currentPlayer = one;
        }

        UpdateCurrentPlayerText();
    }
    
    public void CheckPlayerPoints()
    {
        // If both players can't create more units the game starts.
        if (one.GetPoints() < 10 && two.GetPoints() < 10)
        {
            StartGame();
        }
    }

    public bool IsUnitSelectionScreen()
    {
        return unitSelectionScreen;
    }

    private void UpdateTimer()
    {
        if (gameplayActive && timer > 0f)
        {
            timer -= Time.deltaTime;
            int roundTime = Mathf.RoundToInt(timer);
            gameplayTimerText.text = roundTime.ToString();
            if (roundTime < 4)
            {
                gameplayTimerText.color = Color.red;
            }
        }
        else if (gameplayActive && timer < 0f)
        {
            gameplayActive = false;
            timer = gameplayTime;
            gameplayTimerText.text = "";
            gameplayTimerText.color = Color.white;
            SetUnitSelectionScreenActive();
            FlipPlayer();    
        }
    }
}
