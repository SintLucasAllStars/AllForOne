using System;

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
        isCurrentPlayerOne = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BuyUnit(Unit _unit, int _strength, int _speed, int _range, int _defense)
    {
        try
        {
            if (isCurrentPlayerOne)
            {
                players[0].units.Add(_unit);
                players[0].amountOfPoints -= Unit.Cost(_strength, _speed, _range, _defense);
            }
            else if (!isCurrentPlayerOne)
            {
                players[1].units.Add(_unit);
                players[1].amountOfPoints -= Unit.Cost(_strength, _speed, _range, _defense);
            }
        }
        catch (NullReferenceException a)
        {
            throw a;
        }
    }

    public void MainLoopCountDown()
    {

    }

    public void HaltCountDown(int secondsToWait)
    {
        throw new NotImplementedException();
    }
}
