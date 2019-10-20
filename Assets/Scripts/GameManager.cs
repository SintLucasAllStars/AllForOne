using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState {topview,gameplay};
    public GameState state;

    public Player[] players = new Player[1];
    public Player activePlayer;

    public int[] points = new int[1];

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
        players = GameObject.FindObjectsOfType<Player>();
        activePlayer = players[0];
    }

    private void Update()
    {
        switch (state)
        {
            case GameState.topview:
                if (uiManager.placing)
                {
                    uiManager.PlacingPhase();
                }
                break;
            case GameState.gameplay:
                mainCamera.enabled = false;
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
                return p;
            }
        }
        return null;
    }

}