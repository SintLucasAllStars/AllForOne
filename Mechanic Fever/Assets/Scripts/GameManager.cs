using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private const int MinimumPoints = 10;

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

    void EndCurrentRound()
    {
        Debug.Log("Check");
        IsTurnPlayerOne = !IsTurnPlayerOne;
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
                EndRound += EndCurrentRound;

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
        GameRound currentEvent = (start) ? StartRound : EndRound;
        if(currentEvent != null)
            currentEvent.Invoke();
    }

}
