using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<Player> playerList = new List<Player>();
    public GameObject pawnPrefab;
    public GameObject pawnMenu;
    private void Start() {
        AddPlayer();
        AddPlayer();
        NextTurn();
        ShowMenu();
    }

    public void AddPlayer() {
        playerList.Add(new Player(playerList.Count +1));
    }

    private int playerIndex;
    private Player currentPlayer;
    void NextTurn() {
        currentPlayer = playerList[playerIndex++];
        
        if (playerIndex >= playerList.Count)
            playerIndex = 0;
    }

    private Pawn currentPawn;
    void ShowMenu() {
        if (currentPlayer.canPlacePawns) {
            GameObject pawnInstance = Instantiate(pawnPrefab);
            pawnInstance.transform.position = Vector3.up * 0.5f;
            currentPawn = pawnInstance.GetComponent<Pawn>();
            if (!currentPawn) {
                Debug.LogError("Pawn not found");
            }
            pawnMenu.SetActive(true);
        }
    }
    public void SetPawnHealth(float health) {
        currentPawn.combatUnit.health = health;
        print($"current health {currentPawn.combatUnit.health}");
    }

    public void SetPawnStrength(float strength) {
        currentPawn.combatUnit.strength = strength;
    }

    public void SetPawnDefense(float defense) {
        currentPawn.combatUnit.defense = defense;
    }

    public void SetPawnSpeed(float speed) {
        currentPawn.combatUnit.speed = speed;
    }

}
