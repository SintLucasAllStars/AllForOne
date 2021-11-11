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

    private void Start() {
        AddPlayer(Color.blue);
        AddPlayer(Color.red);
        NextStep();
    }

    void AddPlayer(Color color) {
        playerList.Add(new Player(playerList.Count + 1, color));
    }
    
    void NextTurn() {
        currentPlayer = playerList[playerIndex++];

        if (playerIndex >= playerList.Count)
            playerIndex = 0;
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
        }
    }
}