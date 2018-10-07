using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Instance
    public static GameManager instance;

    //Events
    public enum GameState
    {
        Build, Play, UnitSelection
    }

    public delegate void GameRound(GameState gameState);

    public event GameRound StartRound;
    public event GameRound EndRound;

    //Player
    [SerializeField] Player playerOne;
    [SerializeField] Player playerTwo;
    bool IsPlyerOnesTurn = true;



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

    public void CreateCharacter(Character character)
    {
        ((IsPlyerOnesTurn) ? playerOne : playerTwo).CreateCharacter(character.CalculatePoints());
    }

    public void KillCharacter()
    {
        if(((IsPlyerOnesTurn) ? playerOne : playerTwo).KillCharacter())
        {
            Debug.Log("Game ended! Player: " + ((IsPlyerOnesTurn) ? "Two" : "One") + " Won");
        }
    }

}
