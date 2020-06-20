using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public enum GameState { INSTANTIATINGPLAYERS, P1SETUP, P2SETUP, P1TURN, P2TURN }

public class GameManager : MonoBehaviour
{
    [Header("Player settings.")]
    [SerializeField] private GameObject playerObj;
    [SerializeField] private Transform spawnPosP1;
    [SerializeField] private Transform spawnPosP2;
    public Player player1Manager, player2Manager;
    public List<GameObject> p1Units;
    public List<GameObject> p2Units;

    [Header("Global canvas settings.")]
    public Canvas UiCanvas;
    public GameObject buttonP1Setup;
    public GameObject buttonP2Setup;
    [SerializeField] private TextMeshProUGUI pointsLeftDisplay;
    [SerializeField] private GameObject Narrorator;
    [Header("Unit canvas settings")]
    public GameObject unitStatusDisplay;
    [SerializeField] private TextMeshProUGUI unitTypeDisplay;
    [SerializeField] private TextMeshProUGUI unitTeamDisplay;
    [SerializeField] private TextMeshProUGUI unitHealthDisplay;
    [SerializeField] private TextMeshProUGUI unitStrengthDisplay;
    [SerializeField] private TextMeshProUGUI unitSpeedDisplay;
    [SerializeField] private TextMeshProUGUI unitDefenceDisplay;
    [SerializeField] private Image weakUnitTextImage;
    [SerializeField] private Image strongUnitTextImage;

    [Header("Unit settings")]
    public GameObject weakUnit;
    public GameObject strongUnit;
    public GameObject oldUnit;

    private GameObject player1Obj, player2Obj;
    private Animator player1Anim;
    private Animator player2Anim;
    public string unitType;

    public GameState status;
    
    public GameObject OldUnit { get { return oldUnit; } set { oldUnit = value; } }

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
        unitStatusDisplay.SetActive(false);
        yield return new WaitForSeconds(2f);
        UiCanvas.enabled = true;
        status = GameState.P1SETUP;
        StartCoroutine(DisplayMessage("Player 1:\nPlace all of you minions on the board.\nIf you ran out of points you can press the done button."));
    }

    private void Update()
    {
        if(player1Manager.GetPoints() <= 0)
        {
            unitType = "";
            ShowUnitSetupButtonActive(unitType);
            status = GameState.P2SETUP;
        }

        if (player2Manager.GetPoints() <= 0)
        {
            unitType = "";
            ShowUnitSetupButtonActive(unitType);
            status = GameState.P1TURN;
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

        if(status == GameState.P1TURN)
        {
            buttonP2Setup.SetActive(false);
            buttonP1Setup.SetActive(false);
            player1Manager.enabled = true;
            player2Manager.enabled = false;

            /// Turning on the player1 animation so you can see its he's turn.
            player1Anim.enabled = true;
            player2Anim.enabled = false;
        }
        if(status == GameState.P2TURN)
        {
            buttonP1Setup.SetActive(false);
            buttonP2Setup.SetActive(false);
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
        ShowUnitSetupButtonActive(unitType);
    }

    public void OnWeakUnitButton()
    {
        unitType = "WeakUnit";
        ShowUnitSetupButtonActive(unitType);
    }

    /// Once the button is pressed, change the turn to player 2.
    public void Player1ReadyButton()
    {
        status = GameState.P2SETUP;
        StartCoroutine(DisplayMessage("Player 2:\nPlace all of you minions on the board.\nIf you ran out of points you can press the done button."));
        unitType = "";
        ShowUnitSetupButtonActive(unitType);
    }

    /// Once the button is pressed, change the state to P1Turn!.
    public void Player2ReadyButton()
    {
        status = GameState.P1TURN;
        StartCoroutine(DisplayMessage("Action gameplay comming soon... or not... Stay tuned... or not."));
        unitType = "";
        ShowUnitSetupButtonActive(unitType);
    }

    public void SendMessageToPlayers(string a_Message)
    {
        StartCoroutine(DisplayMessage(a_Message));
    }

    public void ShowUnitStats()
    {
        unitStatusDisplay.SetActive(true);
        Unit oldUnitManager = oldUnit.GetComponent<Unit>();
        unitTypeDisplay.text = oldUnit.gameObject.name;
        unitTeamDisplay.text = "Team: " + oldUnit.gameObject.tag;
        unitHealthDisplay.text = "Hp: " + oldUnitManager.GetHealth().ToString() + " / 100";
        unitStrengthDisplay.text = "Strength: " + oldUnitManager.GetStrength().ToString() + " / 100";
        unitSpeedDisplay.text = "Speed: " + oldUnitManager.GetSpeed().ToString() + " / 100";
        unitDefenceDisplay.text = "Defence: " + oldUnitManager.GetDefence().ToString() + " / 100";
    }

    private void ShowUnitSetupButtonActive(string a_Unittype)
    {
        switch (a_Unittype)
        {
            case "WeakUnit":
                weakUnitTextImage.color = new Color32(4, 166, 6, 255);
                strongUnitTextImage.color = Color.white;
                break;

            case "StrongUnit":
                weakUnitTextImage.color = Color.white;
                strongUnitTextImage.color = new Color32(4, 166, 6, 255);
                break;

            default:
                weakUnitTextImage.color = Color.white;
                strongUnitTextImage.color = Color.white;
                break;
        }
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