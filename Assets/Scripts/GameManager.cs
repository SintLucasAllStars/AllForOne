using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState {Hiring, UnitSelection, Playing};
    public GameState state;

    public Player[] players = new Player[2];
    public Player activePlayer;

    public Transform topViewPos;

    public Camera mainCamera;
    public GameObject purchaseUI;
    public UIManager uiManager;


    #region Singleton
    public static GameManager instance;
    void Awake() {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    private void Start()
    {
        activePlayer = players[0];
    }

    private void Update()
    {
        if (GameManager.instance.players[0].units.Count == 4 && GameManager.instance.players[1].units.Count == 4 && state == GameState.Hiring)
        {
            uiManager.purchaseUI.SetActive(false);
            state = GameState.UnitSelection;
        }

        switch (state)
        {
            case GameState.Hiring:
                if (uiManager.placing && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    uiManager.PlaceUnit();
                }
                break;
            case GameState.UnitSelection:
                break;
            case GameState.Playing:
                break;
            default:
                break;
        }
    }

    public void EndTurn() {
        activePlayer = SwitchPlayer();
    }

    public Player SwitchPlayer() {
        foreach (Player p in players)
        {
            if (p != activePlayer)
            {
                uiManager.activePlayerText.text = activePlayer.name.ToString();
                return p;
            }
        }
        return null;
    }

}