using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject selectedUnit;


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
                if (selectedUnit != null)
                {
                    selectedUnit.GetComponent<Unit>().isActive = true;
                    uiManager.redTeamUI.SetActive(false);
                    uiManager.blueTeamUI.SetActive(false);
                    Debug.Log("Completed UnitSelection PreState");
                    state = GameState.Playing;
                    Debug.Log("Completed UnitSelection postState");
                }
                break;
            case GameState.Playing:
                if (selectedUnit.GetComponent<Unit>().isActive == false)
                {
                    selectedUnit = null;
                    uiManager.redTeamUI.SetActive(true);
                    uiManager.blueTeamUI.SetActive(true);
                    EndTurn();
                    Debug.Log("Completed Playing PreState");
                    state = GameState.UnitSelection;
                    Debug.Log("Completed Playing postState");
                }
                break;
            default:
                break;
        }
    }


    public void EndTurn() {
        activePlayer = SwitchPlayer();
        if (activePlayer == players[1])
        {
            foreach (Button b in uiManager.redUnitButtons)
            {
                b.interactable = true;
            }
            foreach (Button b in uiManager.blueUnitButtons)
            {
                b.interactable = false;
            }
        } else if (activePlayer == players[0])
        {
            foreach (Button b in uiManager.blueUnitButtons)
            {
                b.interactable = true;
            }
            foreach (Button b in uiManager.redUnitButtons)
            {
                b.interactable = false;
            }
        }
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