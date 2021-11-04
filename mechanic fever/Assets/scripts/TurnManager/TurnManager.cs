using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager turnManager;
    public CharacterSelecter characterSelecter;

    public int startingMoney;

    public Player[] players;

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

        players = new Player[2];

        for (int i = 0; i < 2; i++)
        {
            players[i] = new Player(startingMoney);
            print(players[i]);
        }

        StartCoroutine(TurnSystem());
    }

    public GameMode getGamemode()
    {
        return currentGameMode;
    }

    #region currency management
    public Player GetPlayer()
    {
        return players[turn];
    }
    #endregion

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
                print(players[turn]);
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
                turn++;
                if (turn > players.Length - 1)
                {
                    turn = 0;
                }
                yield return new WaitUntil(() => endTurn);
                endTurn = false;
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
        StartCoroutine(TurnSystem());

        //call action fase banner

        currentGameMode = GameMode.action;
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
