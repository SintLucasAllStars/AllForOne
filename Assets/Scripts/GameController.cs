using System;
using UnityEngine;
using System.Collections;
public class GameController : Singleton<GameController>
{
    public int width, depth;

    private Player[] players = new Player[2] {
        new Player(true),
        new Player(false)
    };

    public static bool isCurrentPlayerOne;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyUnit(Unit _unit, int _strength, int _speed, int _range, int _defense)
    {
        try
        {
            if (isCurrentPlayerOne && players[0].amountOfPoints > 10)
            {
                players[0].units.Add(_unit);
                players[0].amountOfPoints -= Unit.Cost(_strength, _speed, _range, _defense);
            }
            else if (!isCurrentPlayerOne && players[0].amountOfPoints > 10)
            {
                players[1].units.Add(_unit);
                players[1].amountOfPoints -= Unit.Cost(_strength, _speed, _range, _defense);
            }
            else
            {
                isCurrentPlayerOne = !isCurrentPlayerOne;
            }
        }
        catch (NullReferenceException a)
        {
            throw a;
        }
    }

    public bool PlaceUnit()
    {
        if (isCurrentPlayerOne)
        {
            if (players[0].units.Count > 0)
            {

            }
        }
        else
        {
            if (players[1].units.Count > 0)
            {

            }
        }
        return false;
    }

    #region CountDown

    IEnumerator Timer()
    {
        int timeLeft = 10;
        UIController.instance.CountDownTimer(timeLeft);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            UIController.instance.CountDownTimer(timeLeft);
        }
        TimersUp();
    }

    private void TimersUp()
    {
        throw new NotImplementedException();
    }

    public void HaltCountDown(int secondsToWait)
    {
        throw new NotImplementedException();
    }
    #endregion
}
