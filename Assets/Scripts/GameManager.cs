using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player one = new Player(1);
    private Player two = new Player(2);
    private Player currentPlayer;
    private bool firstPlayerTurn = true;

    private Color blueText = new Vector4(0.5f, 0.5f, 1f, 1f);

    private float timer = 2f;

    public GameObject unitStoreUI;
    public GameObject CurrentPointsUI;
    public Text currentPlayerText;

    public UnitCreator unitCreator;
    public UnitPlacement unitPlacement;
    
    void Start()
    {
        currentPlayer = one;
    }
    
    void Update()
    {
        //timer -= Time.deltaTime;
    }

    private void StartGame()
    {
        unitStoreUI.SetActive(false);
        CurrentPointsUI.SetActive(false);
        unitCreator.enabled = false;
        unitPlacement.enabled = false;
    }

    public void UpdateCurrentPlayerText()
    {
        currentPlayerText.text = "PLAYER " + currentPlayer.GetPlayerNumber();
        if (currentPlayer == one)
        {
            currentPlayerText.color = Color.red;
        }
        else if (currentPlayer == two)
        {
            currentPlayerText.color = blueText;
        }
    }

    public void SetCurrentPlayer(Player current)
    {
        currentPlayer = current;
    }

    public Player GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void FlipPlayer()
    {
        firstPlayerTurn = !firstPlayerTurn;

        // Flip between players one and two.
        if (!firstPlayerTurn)
        {
            currentPlayer = two;
        }
        else if (firstPlayerTurn)
        {
            currentPlayer = one;
        }
    }
}
