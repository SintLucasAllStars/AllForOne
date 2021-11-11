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

    [Header("costs")] public int healthCost = 1;
    public int defenseCost = 1;
    public int speedCost = 1;
    public int strengthCost = 1;

    private int currentCurrency;

    #region GUI

    [Header("GUI elements - text")]
    public Text guiHealthValue;
    public Text guiDefenseValue;
    public Text guiSpeedValue;
    public Text guiStrengthValue;
    [Space] public Text guiCurrencyValue;
    public Text guiPlayerName;

    [Header("GUI elements - Sliders")]
    public Slider healthSlider;
    public Slider defenseSlider;
    public Slider speedSlider;
    public Slider strengthSlider;
    
    [Header("GUI elements - Buttons")]
    public Button placePawnButton;
    
    #endregion

    private void Start() {
        AddPlayer();
        AddPlayer();
        NextStep();
    }

    void AddPlayer() {
        playerList.Add(new Player(playerList.Count + 1));
    }

    void NextStep() {
        NextTurn();
        if (PlayersCanPlacePawns()) {
            if (currentPlayer.canPlacePawns)
                ShowMenu();
            else {
                NextTurn();
                ShowMenu();
            }
        }
    }

    bool PlayersCanPlacePawns() {
        bool result = false;
        foreach (Player player in playerList) {
            result |= player.canPlacePawns;
        }

        return result;
    }

    private void Battle() {
        print("Batling!");
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
        currentCurrency = currentPlayer.currency;
        guiCurrencyValue.text = currentPlayer.currency.ToString();
        if (currentPlayer.canPlacePawns) {
            GameObject pawnInstance = Instantiate(pawnPrefab);
            pawnInstance.transform.position = Vector3.up * 0.5f;
            pawnInstance.SetActive(false);
            currentPawn = pawnInstance.GetComponent<Pawn>();
            if (!currentPawn) {
                Debug.LogError("Pawn not found");
            }

            UpdateStats();
            pawnMenu.SetActive(true);
        }
    }

    void UpdateStats() {
        // update gui elements
        guiDefenseValue.text = defenseSlider.value.ToString();
        guiHealthValue.text = healthSlider.value.ToString();
        guiSpeedValue.text = speedSlider.value.ToString();
        guiStrengthValue.text = strengthSlider.value.ToString();

        // ensure the pawn stats correspond with gui
        CombatUnit cu = currentPawn.combatUnit;
        cu.defense = (int)defenseSlider.value;
        cu.health = (int)healthSlider.value;
        cu.speed = (int)speedSlider.value;
        cu.strength = (int)strengthSlider.value;
        CalculateNewCurrency();
    }

    public void EndTurn() {
        currentPlayer.canPlacePawns = false;
        pawnMenu.SetActive(false);
        NextStep();
    }

    private IEnumerator PositionPawn() {
        bool placed = false;
        while (!placed) {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayermask)) {
                currentPawn.transform.position = hit.point + Vector3.up * 0.5f;
            }

            if (Input.GetMouseButtonDown(0)) {
                placed = true;
            }

            yield return 0;
        }
    }

    // Called from gui
    public void PlacePawn() {
        StartCoroutine(PlacePawnCoroutine());
    }

    IEnumerator PlacePawnCoroutine() {
        pawnMenu.SetActive(false);
        currentPawn.gameObject.SetActive(true);
        currentPlayer.currency = currentCurrency;
        print($"{currentPlayer.name} currency: {currentPlayer.currency} local currency {currentCurrency}");
        yield return StartCoroutine(PositionPawn());
        NextStep();
    }


    void CalculateNewCurrency() {
        int cost;
        CombatUnit cu = currentPawn.combatUnit;
        cost = cu.defense + cu.health + cu.speed + cu.strength;
        currentCurrency = currentPlayer.currency - cost;
        placePawnButton.gameObject.SetActive(currentCurrency >= 0);
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
        guiStrengthValue.text = strength.ToString();
        CalculateNewCurrency();
    }

    public void SetPawnDefense(float defense) {
        currentPawn.combatUnit.defense = (int)defense;
        guiDefenseValue.text = defense.ToString();
        CalculateNewCurrency();
    }

    public void SetPawnSpeed(float speed) {
        currentPawn.combatUnit.speed = (int)speed;
        guiSpeedValue.text = speed.ToString();
        CalculateNewCurrency();
    }
}