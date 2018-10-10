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
    bool IsTurnPlayerOne = true;

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
                if(EndRound != null)
                    EndRound.Invoke();
                return false;
            }
        }

        return true;
    }

    public void KillCharacter()
    {
        if(GetCurrentPlayer().KillCharacter())
        {
            Debug.Log("Game ended! Player: " + ((IsTurnPlayerOne) ? "Two" : "One") + " Won");
        }
    }

    public Player GetCurrentPlayer()
    {
        return (IsTurnPlayerOne) ? playerOne : playerTwo;
    }

    public void RunEvent(bool start)
    {
        if(!start)
           IsTurnPlayerOne = !IsTurnPlayerOne;

        GameRound currentEvent = (start) ? StartRound : EndRound;
        if(currentEvent != null)
            currentEvent.Invoke();
    }



}
