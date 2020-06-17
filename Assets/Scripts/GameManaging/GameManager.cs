using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public enum GameState { INSTANTIATINGPLAYERS, P1SETUP, P2SETUP, P1TURN, P2TURN, FIGHT }

public class GameManager : MonoBehaviour
{
    [Header("Player settings.")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Transform spawnPosP1;
    [SerializeField] private Transform spawnPosP2;
    public Player player1Manager, player2Manager;
    public List<GameObject> p1Units;
    public List<GameObject> p2Units;

    [Header("Canvas settings.")]
    public Canvas UiCanvas;
    public GameObject buttonP1Setup;
    public GameObject buttonP2Setup;
    [SerializeField] private TextMeshProUGUI pointsLeftDisplay;
    [SerializeField] private GameObject Narrorator;

    [Header("Unit settings")]
    public GameObject weakUnit;
    public GameObject strongUnit;

    private GameObject player1Obj, player2Obj;
    private Animator player1Anim;
    private Animator player2Anim;
    public string unitType;

    public GameState status;

    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this);
        }

        p1Units = new List<GameObject>();
        p2Units = new List<GameObject>();
    }
    #endregion

    private IEnumerator Start()
    {
        status = GameState.INSTANTIATINGPLAYERS;
        SetupCharacters();
        UiCanvas.enabled = false;
        player1Anim.enabled = false;
        player2Anim.enabled = false;
        yield return new WaitForSeconds(2f);
        UiCanvas.enabled = true;
        status = GameState.P1SETUP;
        StartCoroutine(DisplayMessage("Player 1:\nPlace all of you minions on the board.\nIf you ran out of points you can press the done button."));
    }

    private void Update()
    {
        if(player1Manager.GetPoints() <= 0)
        {
            status = GameState.P2SETUP;
        }

        if (player2Manager.GetPoints() <= 0)
        {
            status = GameState.FIGHT;
        }

        /// Turning scripts off because else p2 will lose points at the same time when you 
        if (status == GameState.P1SETUP)
        {
            ///Showing the score of player 1.
            pointsLeftDisplay.text = "Player 1 points left: " + player1Manager.GetPoints();

            /// Turning the button off for player 2 and showing player 1's button.
            /// Dissabling the Player 2 manager.
            buttonP2Setup.SetActive(false);
            buttonP1Setup.SetActive(true);
            player1Manager.enabled = true;
            player2Manager.enabled = false;

            /// Turning on the player1 animation so you can see its he's turn.
            player1Anim.enabled = true;
            player2Anim.enabled = false;
        }
        else if (status == GameState.P2SETUP)
        {
            ///Showing the score of player 2.
            pointsLeftDisplay.text = "Player 2 points left: " + player2Manager.GetPoints();

            /// Turning the button off for player 1 and showing player 2's button.
            /// Dissabling the Player 1 manager.
            buttonP1Setup.SetActive(false);
            buttonP2Setup.SetActive(true);
            player1Manager.enabled = false;
            player2Manager.enabled = true;

            /// Turning on the player 2 animation so you can see its he's turn.
            player1Anim.enabled = false;
            player2Anim.enabled = true;
        }
    }

    private void SetupCharacters()
    {
        player1Obj = Instantiate(playerObj, spawnPosP1.transform.position, spawnPosP1.transform.rotation);
        player1Manager = player1Obj.AddComponent<Player>();
        player1Manager.Initialize(1);
        player1Obj.name = "Player 1";
        player1Anim = player1Obj.GetComponent<Animator>();

        player2Obj = Instantiate(playerObj, spawnPosP2.transform.position, spawnPosP2.transform.rotation);
        player2Manager = player2Obj.AddComponent<Player>();
        player2Manager.Initialize(2);
        player2Obj.name = "Player 2";
        player2Anim = player2Obj.GetComponent<Animator>();
    }

    public void OnStrongUnitButton()
    {
        unitType = "StrongUnit";
    }

    public void OnWeakUnitButton()
    {
        unitType = "WeakUnit";
    }

    /// Once the button is pressed, change the turn to player 2.
    public void Player1ReadyButton()
    {
        status = GameState.P2SETUP;
        StartCoroutine(DisplayMessage("Player 2:\nPlace all of you minions on the board.\nIf you ran out of points you can press the done button."));
    }

    /// Once the button is pressed, change the state to fight!.
    public void Player2ReadyButton()
    {
        status = GameState.FIGHT;
    }

    /// <summary>
    /// Tell the users about what they have to do!
    /// </summary>
    /// <param name="a_Text">Give us information about what text we should display!</param>
    /// <returns></returns>
    private IEnumerator DisplayMessage(string a_Text)
    {
        Narrorator.SetActive(true);
        Narrorator.GetComponentInChildren<TextMeshProUGUI>().text = a_Text;
        yield return new WaitForSeconds(5);
        Narrorator.GetComponentInChildren<TextMeshProUGUI>().text = "";
        Narrorator.SetActive(false);
    }
}