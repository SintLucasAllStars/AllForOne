using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("PlayerTurn")]
    public playerTurn currentPlayer;
    public enum playerTurn { RedTeam, BlueTeam}

    [Header("GameState")]
    public currentState gameState;
    public enum currentState { Picking, Playing }

    [Header("Text & Mask & Cam")]
    public GameObject overviewCam;
    public LayerMask roofIgnore;
    public Text whoseTurnText;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        TurnEnded();
    }

    private void Update()
    {
        if (gameState == currentState.Picking)
        {
            UnitSelect();
        }
    }

    public void TurnEnded()
    {
        overviewCam.SetActive(true);
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
