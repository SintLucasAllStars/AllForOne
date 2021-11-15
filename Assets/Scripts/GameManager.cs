using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
    public GameObject playerDisplay;
    public GameObject winScreen;
    public GameObject menuScreen;
    public Text currentPlayerText;
    public Text gameplayTimerText;
    public Text winText;
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
        CheckEscape();
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

    private void GameFinished()
    {
        playerDisplay.SetActive(false);
        unitCam.enabled = false;
        gameplayActive = false;

        if (one.GetList().Count > 0)
        {
            winText.text = "PLAYER 1 WON";
            winText.color = Color.red;
            
        }
        else if (two.GetList().Count > 0)
        {
            winText.text = "PLAYER 2 WON";
            winText.color = blueText;
        }
        winScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitMenu()
    {
        menuScreen.SetActive(false);
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
            CheckRoundEnd();
        }
    }

    private void CheckRoundEnd()
    {
        DestroyUnitOutside();

        if (one.GetList().Count <= 0 || two.GetList().Count <= 0)
        {
            GameFinished();
        }
        else
        {
            SetUnitSelectionScreenActive();
            FlipPlayer();
        }        
    }

    private void DestroyUnitOutside()
    {
        if (activeUnit.CheckIfInside() == false)
        {
            // Remove unit from player and remove it from the scene.
            currentPlayer.RemoveUnit(activeUnit.gameObject);
            Destroy(activeUnit.gameObject);
        }
    }

    private void CheckEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuScreen.activeSelf)
            {
                menuScreen.SetActive(true);
            }
            else if (menuScreen.activeSelf)
            {
                menuScreen.SetActive(false);
            }
        }
    }
}
