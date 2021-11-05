using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager turnManager;
    
    private CharacterSelecter characterSelecter;

    [Header("The Amount of players in game")]
    [Space(10)]
    public int playerAmount;
    [Header("The amount of money each player starts with")]
    [Space(10)]
    public int startingMoney;
    [Space(30)]
    public Vector2 fieldSize;

    private Player[] players;

    public bool controllingCamera = true;

    public bool timerDone = true;
    public bool turnTimerPaused;
    public float timePerTurn;
    private float timer;

    private bool gameOver;

    private bool endTurn;

    public enum GameMode
    {
        setup,
        action
    }

    public GameMode currentGameMode;

    public int turn = 0;

    private void Awake()
    {
        if (turnManager is null)
        {
            turnManager = this;
        }
        else
        {
            Destroy(this);
        }

        characterSelecter = GetComponent<CharacterSelecter>();

        startGame();
    }

    private void startGame()
    {
        players = new Player[playerAmount];

        for (int i = 0; i < playerAmount; i++)
        {
            players[i] = new Player(startingMoney);
        }

        StartCoroutine(TurnSystem());
    }

    public GameMode getGamemode()
    {
        return currentGameMode;
    }

    public Player GetPlayer()
    {
        return players[turn];
    }

    #region turn management
    public int getTurnIndex()
    {
        return turn;
    }

    private IEnumerator TurnSystem()
    {
        while (!gameOver)
        {
            if (currentGameMode == GameMode.setup)
            {
                if (turn > players.Length - 1)
                {
                    turn = 0;
                }
                yield return new WaitUntil(() => endTurn || players[turn].getCurrency() <= 0);
                endTurn = false;
                turn++;

                if (Array.TrueForAll(players, n => n.getCurrency() <= 0))
                {
                    EndSetupFase();
                }
            }
            else
            {
                if (turn > players.Length - 1)
                {
                    turn = 0;
                }
                yield return new WaitUntil(() => endTurn);
                endTurn = false;
                turn++;
            }
        }
    }

    public void startTimer()
    {
        timerDone = false;
        timer = timePerTurn;
    }

    private void Update()
    {
        if (!turnTimerPaused && timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0 && !timerDone)
        {
            timerDone = true;
            StartCoroutine(characterSelecter.resetCamera());
            characterSelecter.selectedCharacter.GetComponent<CharacterController>().resetCharacter();
            EndTurn();
        }
    }
    #endregion

    #region turn and fase ending

    public void PlayerDoneSetupFase()
    {
        GetPlayer().zeroCurrency();
    }

    public void EndSetupFase()
    {
        StopAllCoroutines();
        currentGameMode = GameMode.action;
        turn = 0;
        StartCoroutine(TurnSystem());

        //call action fase banner
    }

    public void EndTurn()
    {
        endTurn = true;
    }

    public void EndGame()
    {
        gameOver = true;
    }
    #endregion
}
