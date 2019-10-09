using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("PlayerTurn")]
    public playerTurn currentPlayer;
    public enum playerTurn { RedTeam, BlueTeam }

    [Header("AmountOfAlivePlayers")]
    public int teamBlue;
    public int teamRed;

    [Header("GameState")]
    public currentState gameState;
    public enum currentState { Picking, Playing, Won }

    [Header("References")]
    public GameObject overviewCam;
    public Animator deathObject;
    public LayerMask roofIgnore;
    public Text whoseTurnText;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UnitController[] unitcontrollers = UnityEngine.Object.FindObjectsOfType<UnitController>();
        foreach (UnitController unit in unitcontrollers)
        {
            if (unit.teamName == "Red Team")
            {
                teamRed++;
            }
            if (unit.teamName == "Blue Team")
            {
                teamBlue++;
            }
        }
        TurnEnded();
    }

    private void Update()
    {
        if (gameState == currentState.Picking)
        {
            UnitSelect();
        }
    }

    public void CheckIfWon()
    {
        if (teamBlue < 1)
        {
            whoseTurnText.text = "Team Red WON";
            gameState = currentState.Won;
        }
        if (teamRed < 1)
        {
            whoseTurnText.text = "Team Blue WON";
            gameState = currentState.Won;
        }
    }

    public void TurnEnded()
    {
        overviewCam.SetActive(true);
        deathObject.Play("Death");
        gameState = currentState.Picking;
        switch (currentPlayer)
        {
            case playerTurn.RedTeam:
                currentPlayer = playerTurn.BlueTeam;
                whoseTurnText.text = "Blue Player Turn";
                break;
            case playerTurn.BlueTeam:
                currentPlayer = playerTurn.RedTeam;
                whoseTurnText.text = "Red Player Turn";
                break;
            default:
                break;
        }
    }

    void UnitSelect()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, roofIgnore) && currentPlayer == playerTurn.BlueTeam)
            {
                if (hit.collider.tag == "Blue Team")
                {
                    UnitController ut = hit.collider.GetComponent<UnitController>();
                    overviewCam.SetActive(false);
                    gameState = currentState.Playing;
                    ut.GainControl();
                }
            }
            if (Physics.Raycast(ray, out hit, roofIgnore) && currentPlayer == playerTurn.RedTeam)
            {
                if (hit.collider.tag == "Red Team")
                {
                    UnitController ut = hit.collider.GetComponent<UnitController>();
                    overviewCam.SetActive(false);
                    gameState = currentState.Playing;
                    ut.GainControl();
                }
            }
        }
    }

}
