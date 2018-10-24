using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager instance;

    //Events
    public delegate void GameRound();

    public event GameRound StartRound;
    public event GameRound EndRound;

    //Player
    [SerializeField] private Player playerOne;
    [SerializeField] private Player playerTwo;
    public bool IsTurnPlayerOne = true;

    //Game
    public Timer timer;
    public bool activeGame = false;
    public bool gameOver = false;

    // Use this for initialization
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if(!activeGame)
            return;

        timer.Tick();
    }

    public bool CheckCharacterPoints(int points)
    {
        return GetCurrentPlayer().CheckPoints(points);
    }

    public bool CreateCharacter(int costPoints)
    {
        Player currentPlayer = GetCurrentPlayer();
        currentPlayer.CreateCharacter(costPoints);

        IsTurnPlayerOne = !IsTurnPlayerOne;
        if(GetCurrentPlayer().isOutOfPoints)
        {
            if(!currentPlayer.isOutOfPoints)
                IsTurnPlayerOne = !IsTurnPlayerOne;
            else
            {
                IsTurnPlayerOne = true;
                if(EndRound != null)
                    EndRound.Invoke();

                timer.Initialize();
                return false;
            }
        }

        return true;
    }

    public void KillCharacter()
    {
        if(GetOtherPlayer().KillCharacter())
        {
            gameOver = true;
            GameUi.instance.EndScreen(GetCurrentPlayer());
        }
    }

    public void KillCharacter(string character)
    {
        if(character == "PlayerOne")
            playerOne.KillCharacter();
        else if(character == "PlayerTwo")
            playerTwo.KillCharacter();
    }

    public Player GetCurrentPlayer()
    {
        return (IsTurnPlayerOne) ? playerOne : playerTwo;
    }

    public Player GetOtherPlayer()
    {
        return (IsTurnPlayerOne) ? playerTwo : playerOne;
    }

    public void RunEvent(bool start)
    {
        if(gameOver) return;

        if(!start)
            IsTurnPlayerOne = !IsTurnPlayerOne;

        activeGame = start;

        GameRound currentEvent = (start) ? StartRound : EndRound;
        if(currentEvent != null)
            currentEvent.Invoke();
    }



}
