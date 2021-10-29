using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager turnManager;

    public int player1Currency;
    public int player2Currency;

    public int powerUpIndex = 0;

    public List<PowerUp> player1Powerup = new List<PowerUp>();
    public List<PowerUp> player2Powerup = new List<PowerUp>();

    public bool controllingCamera = true;

    private bool gameOver;

    private bool endTurn;

    public enum GameMode
    {
        setup,
        action
    }

    public GameMode currentGameMode;

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

        StartCoroutine(TurnSystem());
    }

    public GameMode getGamemode()
    {
        return currentGameMode;
    }

    #region currency management

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

    #endregion

    #region turn management
    public int getTurnIndex()
    {
        return (int)currentTurn;
    }

    public Turns GetCurrentTurn()
    {
        return currentTurn;
    }
    public Turns getInversedTurn()
    {
        Turns value;

        switch ((int)currentTurn)
        {
            case 1:
                value = Turns.player2;
                break;
            case 2:
                value = Turns.player1;
                break;
            default:
                Debug.LogError($"Returned Defaulted Inversed Turn, (line: 127, {this.ToString()})");
                value = Turns.player1;
                break;
        }

        return value;
    }

    private IEnumerator TurnSystem()
    {
        while (!gameOver)
        {
            if (currentGameMode == GameMode.setup)
            {
                currentTurn = Turns.player1;
                yield return new WaitUntil(() => endTurn || player1Currency <= 0);
                endTurn = false;
                currentTurn = Turns.player2;
                yield return new WaitUntil(() => endTurn || player2Currency <= 0);
                endTurn = false;

                if (player1Currency <= 0 && player2Currency <= 0)
                {
                    EndSetupFase();
                }
            }
            else
            {
                currentTurn = Turns.player1;
                yield return new WaitUntil(() => endTurn);
                endTurn = false;
                currentTurn = Turns.player2;
                yield return new WaitUntil(() => endTurn);
                endTurn = false;
            }
        }
    }
    #endregion

    #region turn and fase ending

    public void PlayerDoneSetupFase()
    {
        switch ((int)currentTurn)
        {
            case 1:
                player1Currency = 0;
                break;
            case 2:
                player2Currency = 0;
                break;
        }
    }

    public void EndSetupFase()
    {
        StopAllCoroutines();
        StartCoroutine(TurnSystem());

        //call action fase banner

        currentGameMode = GameMode.action;
    }

    public void EndTurn()
    {
        endTurn = true;
    }

    public void EndGame()
    {
        gameOver = true;
    }
    #endregion

    #region powerUps
    public List<PowerUp> getPowerUpList()
    {
        List<PowerUp> value;

        switch ((int)currentTurn)
        {
            case 1:
                value = player1Powerup;
                break;
            case 2:
                value = player2Powerup;
                break;
            default:
                Debug.LogError($"Returned Defaulted Inversed Turn, (line: 216, {ToString()})");
                value = player1Powerup;
                break;
        }

        return value;
    }

    public void IncreasePowerupIndex()
    {
        int count = getPowerUpList().Count;
        powerUpIndex++;
        if (powerUpIndex > count)
        {
            powerUpIndex = 0;
        }
    }

    public void DecreasePowerupIndex()
    {
        int count = getPowerUpList().Count;
        powerUpIndex--;
        if (powerUpIndex < 0)
        {
            powerUpIndex = count;
        }
    }

    public PowerUp GetPowerUp()
    {
        return getPowerUpList()[powerUpIndex];
    }

    public void AddPowerUp(PowerUp powerup)
    {
        getPowerUpList().Add(powerup);
    }

    public void RemovePowerUp(PowerUp powerUp)
    {
        getPowerUpList().Remove(powerUp);
    }
    #endregion
}
