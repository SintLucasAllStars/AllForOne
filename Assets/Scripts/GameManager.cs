using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<Player> playerList = new List<Player>();
    public GameObject pawnPrefab;
    public GameObject pawnMenu;
    public Camera camera;
    public LayerMask floorLayermask;
    private void Start() {
        AddPlayer();
        AddPlayer();
        NextTurn();
        ShowMenu();
    }

    void AddPlayer() {
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
            pawnInstance.SetActive(false);
            currentPawn = pawnInstance.GetComponent<Pawn>();
            if (!currentPawn) {
                Debug.LogError("Pawn not found");
            }
            pawnMenu.SetActive(true);
        }
    }

    public void CloseMenu() {

        currentPlayer.canPlacePawns = false;
        pawnMenu.SetActive(false);
    }

    public void PlacePawn() {
        pawnMenu.SetActive(false);
        currentPawn.gameObject.SetActive(true);
        StartCoroutine("PositionPawn");
    }

    private IEnumerator PositionPawn() {
        bool placed = false;
        while (!placed) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayermask )) {
                currentPawn.transform.position = hit.point;
                print(hit.transform.position);
            }
            if (Input.GetMouseButtonDown(0)) {
                placed = true;
            }

            yield return 0;
        }
    }
    
    public void SetPawnHealth(float health) {
        currentPawn.combatUnit.health = health;
        //print($"current health {currentPawn.combatUnit.health}");
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
