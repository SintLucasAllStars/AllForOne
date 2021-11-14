using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public List<Player> playerList = new List<Player>();

    [HideInInspector]
    public int playerIndex;

    public Player currentPlayer;

    [HideInInspector]
    public PlacementPhase placementPhase;

    [HideInInspector]
    public BattlePhase battlePhase;

    public Text winText;

    public Camera camera;
    public AudioSource audioSource;

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
        playerList.Add(new Player(playerList.Count + 1, color, this));
    }

    public void NextTurn() {
        currentPlayer = playerList[playerIndex++];

        if (playerIndex >= playerList.Count)
            playerIndex = 0;
        battlePhase.GuiUpdatePlayer();
    }

    public void StartWinstate() {
        StartCoroutine(Winstate());
    }

    public IEnumerator Winstate() {
        foreach (Player player in playerList) {
            if (player.pawns.Count > 0) {
                winText.gameObject.SetActive(true);
                winText.color = player.color;
                winText.text = $"You're winner\n{player.name}!";
                transitionTime = 0;
                StartCoroutine(DecreasePitch());
                yield return new WaitForSeconds(3);
                while (true) {
                    if (Input.anyKey) {
                        Cursor.lockState = CursorLockMode.None;
                        SceneManager.LoadScene(0);
                    }

                    yield return null;
                }
            }
        }
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
            StartCoroutine(IncreasePitch());
        }
    }

    private float transitionTime = 0;

    IEnumerator IncreasePitch() {
        while (transitionTime <= 1) {
            audioSource.pitch = Mathf.Lerp(0.4f, 1, transitionTime);
            transitionTime += 0.5f * Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DecreasePitch() {
        while (transitionTime <= 1) {
            audioSource.pitch = Mathf.Lerp(1, 0.4f, transitionTime);
            transitionTime += 1 * Time.deltaTime;
            yield return null;
        }
    }
}