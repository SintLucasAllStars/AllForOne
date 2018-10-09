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
    [SerializeField] Player playerOne;
    [SerializeField] Player playerTwo;
    bool isPlyerOnesTurn = true;



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

    // Update is called once per frame
    void Update()
    {

    }

    public bool CheckCharacterPoints(int points)
    {
        return GetCurrentPlayer().CheckPoints(points);
    }


    public void CreateCharacter(int costPoints)
    {
        Player currentPlayer = GetCurrentPlayer();
        currentPlayer.CreateCharacter(costPoints);

        isPlyerOnesTurn = !isPlyerOnesTurn;
        if(GetCurrentPlayer().isOutOfPoints)
        {
            if(!currentPlayer.isOutOfPoints)
                isPlyerOnesTurn = !isPlyerOnesTurn;
            else
                StartRound.Invoke();
        }

    }

    public void KillCharacter()
    {
        if(GetCurrentPlayer().KillCharacter())
        {
            Debug.Log("Game ended! Player: " + ((isPlyerOnesTurn) ? "Two" : "One") + " Won");
        }
    }

    Player GetCurrentPlayer()
    {
        return (isPlyerOnesTurn) ? playerOne : playerTwo;
    }

}
