using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacementPhase : MonoBehaviour {
    public GameObject pawnPrefab;
    public GameObject pawnMenu;
    public LayerMask floorLayermask;
    [HideInInspector]
    public GameManager gameManager;

    [Header("costs")]
    int cost;
    public int healthCost = 3;
    public int defenseCost = 2;
    public int speedCost = 3;
    public int strengthCost = 2;

    private int currentCurrency;

    #region GUI

    [Header("GUI elements - text")]
    public Text guiHealthValue;

    public Text guiDefenseValue;
    public Text guiSpeedValue;
    public Text guiStrengthValue;

    [Space]
    public Text guiCurrencyValue;

    public Text guiPlayerName;

    [Header("GUI elements - Sliders")]
    public Slider healthSlider;

    public Slider defenseSlider;
    public Slider speedSlider;
    public Slider strengthSlider;

    [Header("GUI elements - Buttons")]
    public Button placePawnButton;

    #endregion

    public bool PlayersCanPlacePawns() {
        bool result = false;
        foreach (Player player in gameManager.playerList) {
            result |= player.canPlacePawns;
        }

        return result;
    }

    private Pawn currentPawn;

    public void ShowPawnMenu() {
        guiPlayerName.text = gameManager.currentPlayer.name;
        guiPlayerName.color = gameManager.currentPlayer.color;
        currentCurrency = gameManager.currentPlayer.currency;
        guiCurrencyValue.text = gameManager.currentPlayer.currency.ToString();
        if (gameManager.currentPlayer.canPlacePawns) {
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
        gameManager.currentPlayer.canPlacePawns = false;
        pawnMenu.SetActive(false);
        gameManager.NextStep();
    }

    private IEnumerator PositionPawn() {
        bool placed = false;
        while (!placed) {
            Ray ray = gameManager.camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayermask)) {
                currentPawn.transform.position = hit.point + Vector3.up * 0.5f;
            }

            if (Input.GetButton("Fire1")) {
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
        currentPawn.GetComponentInChildren<MeshRenderer>().material.color = gameManager.currentPlayer.color;
        currentPawn.gameObject.SetActive(true);
        currentPawn.player = gameManager.currentPlayer;
        gameManager.currentPlayer.pawns.Add(currentPawn);
        gameManager.currentPlayer.currency = currentCurrency;
        if (gameManager.currentPlayer.currency == 0)
            gameManager.currentPlayer.canPlacePawns = false;
        print($"{gameManager.currentPlayer.name} currency: {gameManager.currentPlayer.currency} local currency {currentCurrency}");
        yield return StartCoroutine(PositionPawn());
        gameManager.NextStep();
    }

    void CalculateNewCurrency() {
        CombatUnit cu = currentPawn.combatUnit;
        cost = (cu.defense * defenseCost) + (cu.health * healthCost) + (cu.speed * speedCost) + (cu.strength * strengthCost);
        currentCurrency = gameManager.currentPlayer.currency - cost;
        placePawnButton.gameObject.SetActive(currentCurrency >= 0);
        guiCurrencyValue.text = $"{gameManager.currentPlayer.currency.ToString()} / {currentCurrency.ToString()}";
    }

    #region SliderNumberUpdate

    public void SetPawnHealth(float health) {
        currentPawn.combatUnit.health = (int)health;
        guiHealthValue.text = health.ToString();
        CalculateNewCurrency();
        //print($"current health {currentPawn.combatUnit.health}");
    }

    public void SetPawnStrength(float strength) {
        currentPawn.combatUnit.strength = (int)strength;
        guiStrengthValue.text = strength.ToString();
        currentPawn.transform.localScale = (.5f + .1f * currentPawn.combatUnit.strength) * Vector3.one;
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

    #endregion

    private void Battle() {
        print("Batling!");
    }
}