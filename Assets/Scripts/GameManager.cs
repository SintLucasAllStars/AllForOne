using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private List<Player> playerList = new List<Player>();
    public GameObject pawnPrefab;
    public GameObject pawnMenu;
    public Camera camera;
    public LayerMask floorLayermask;

    [Header("costs")]
    public int healthCost = 1;
    public int defenseCost = 1;
    public int speedCost = 1;
    public int strengthCost = 1;

    private int currentCurrency;
    
    [Header("GUI elements")]
    public Text guiHealthValue;
    public Text guiCurrencyValue;
    public Text guiPlayerName;
    
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
        guiPlayerName.text = currentPlayer.name;
        guiCurrencyValue.text = currentPlayer.currency.ToString();
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
        currentPlayer.currency = currentCurrency;
        StartCoroutine("PositionPawn");
    }

    private IEnumerator PositionPawn() {
        bool placed = false;
        while (!placed) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayermask )) {
                currentPawn.transform.position = hit.point + Vector3.up * 0.5f;
            }
            if (Input.GetMouseButtonDown(0)) {
                placed = true;
            }

            yield return 0;
        }
    }

    void CalculateNewCurrency() {
        int cost;
        CombatUnit cu = currentPawn.combatUnit;
        cost = cu.defense + cu.health + cu.speed + cu.strength;
        currentCurrency = currentPlayer.currency - cost;
        guiCurrencyValue.text = currentCurrency.ToString();
    }
    
    public void SetPawnHealth(float health) {
        currentPawn.combatUnit.health = (int)health;
        guiHealthValue.text = health.ToString();
        CalculateNewCurrency();
        //print($"current health {currentPawn.combatUnit.health}");
    }

    public void SetPawnStrength(float strength) {
        currentPawn.combatUnit.strength = (int)strength;
        CalculateNewCurrency();
    }

    public void SetPawnDefense(float defense) {
        currentPawn.combatUnit.defense = (int)defense;
        CalculateNewCurrency();
    }

    public void SetPawnSpeed(float speed) {
        currentPawn.combatUnit.speed = (int)speed;
        CalculateNewCurrency();
    }

}
