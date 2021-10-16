using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager turnManager;

    public int player1Currency;
    public int player2Currency;

    private bool gameOver;

    private bool endTurn;

    private enum GameMode
    {
        setup,
        action
    }

    private GameMode currentGameMode;

    public enum Turns
    {
        player1 = 1,
        player2 = 2,
    }

    public Turns currentTurn;

    private void Awake()
    {
        if (turnManager is null)
        {
            turnManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TurnSystem());
    }

    public int getCurrency()
    {
        int value = 0;

        switch ((int)currentTurn)
        {
            case 1:
                value = player1Currency;
                break;
            case 2:
                value = player2Currency;
                break;
        }

        return value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            print(BuyCharacter(10));
            EndTurn();
        }
    }

    private IEnumerator TurnSystem()
    {
        while (!gameOver)
        {
            currentTurn = Turns.player1;
            yield return new WaitUntil(() => endTurn);
            endTurn = false;
            currentTurn = Turns.player2;
            yield return new WaitUntil(() => endTurn);
            endTurn = false;
        }

        //TODO: endGame
    }

    public bool BuyCharacter(int cost)
    {
        bool value = false;

        switch ((int)currentTurn)
        {
            case 1:
                if (cost <= player1Currency)
                {
                    player1Currency -= cost;
                    value = true;
                }
                break;
            case 2:
                if (cost <= player2Currency)
                {
                    player2Currency -= cost;
                    value = true;
                }
                break;
        }

        return value;
    }

    public void EndTurn()
    {
        endTurn = true;
    }

    public void EndGame()
    {
        gameOver = true;
    }
}
