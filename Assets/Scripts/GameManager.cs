using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<Player> playerList = new List<Player>();
    [HideInInspector]
    public int playerIndex;
    public Player currentPlayer;
    [HideInInspector]
    public PlacementPhase placementPhase;
    [HideInInspector]
    public BattlePhase battlePhase;

    public Camera camera;

    private void Awake() {
        placementPhase = FindObjectOfType<PlacementPhase>();
        placementPhase.gameManager = this;
        
        battlePhase = FindObjectOfType<BattlePhase>();
        battlePhase.gameManager = this;
    }

    private void Start() {
        AddPlayer(Color.blue);
        AddPlayer(Color.red);
        NextStep();
    }

    void AddPlayer(Color color) {
        playerList.Add(new Player(playerList.Count + 1, color));
    }

    public void NextTurn() {
        currentPlayer = playerList[playerIndex++];

        if (playerIndex >= playerList.Count)
            playerIndex = 0;
        battlePhase.GuiUpdatePlayer();
    }

    public void NextStep() {
        NextTurn();
        if (placementPhase.PlayersCanPlacePawns()) {
            if (currentPlayer.canPlacePawns)
                placementPhase.ShowPawnMenu();
            else {
                NextTurn();
                placementPhase.ShowPawnMenu();
            }
        }
        else {
            StartCoroutine(battlePhase.InitBattle());
        }
    }
}