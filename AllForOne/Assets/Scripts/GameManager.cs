using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStates {Create,Place, Select,Move}
public enum TurnState {Player1,Player2}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameStates gamestate;
    public TurnState turn;
    public GameObject buyWindow;
    public Transform characterParent;
    public Player player1 = new Player(100, "Player1");
    public Player player2 = new Player(100, "Player2");

    private float points;
    public Player curPlayer;
    public GameObject selectedPlayer;


    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(instance);
        }
    }

    void Update()
    {
        GameStateManager();
        TurnManager();
    }

    public void UpdateBuyWindow(bool state)
    {
        buyWindow.SetActive(state);
    }

    public void SwitchState(GameStates state)
    {
        gamestate = state;
    }

    public void PlaceCharacter()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            CreateActor.instance.actor.transform.parent = characterParent;
            CreateActor.instance.actor.layer = 0;
            SwitchState(GameStates.Create);
            NextTurn();
        }
    }

    public void GameStateManager()
    {
        switch (gamestate)
        {
            case GameStates.Create:
                UpdateBuyWindow(true);
                if(player1.ready && player2.ready)
                {
                    SwitchState(GameStates.Select);
                }
                break;
            case GameStates.Place:
                UpdateBuyWindow(false);
                PlaceCharacter();
                break;
            case GameStates.Select:
                UpdateBuyWindow(false);

                break;
            case GameStates.Move:
                UpdateBuyWindow(false);
                MoveSelectedPlayer(selectedPlayer);
                break;
        }
    }

    public void TurnManager()
    {
        switch (turn)
        {
            case TurnState.Player1:
                curPlayer = player1;
                break;
            case TurnState.Player2:
                curPlayer = player2;
                break;
        }
    }

    public void NextTurn()
    {
        switch (turn)
        {
            case TurnState.Player1:
                turn = TurnState.Player2;
                break;
            case TurnState.Player2:
                turn = TurnState.Player1;
                break;
        }
    }
    public void ReadyUp()
    {
        curPlayer.ready = true;
        NextTurn();
    }

    public void SelectPlayer(GameObject selectedPlayer)
    {
        this.selectedPlayer = selectedPlayer;
        CameraBehaviour.instance.target = selectedPlayer.transform;
        SwitchState(GameStates.Move);

    }

    public void MoveSelectedPlayer(GameObject selectedPlayer)
    {
        Character player = selectedPlayer.GetComponent<Character>();
        player.characterState = CharacterStates.Moving;
    }

}
