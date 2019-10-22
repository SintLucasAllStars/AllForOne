using System.Collections.Generic;

public class Player
{
    private bool isPlayerOne;

    public int amountOfPoints = 100;

    public List<Unit> units;
    private bool myTurn;
    private bool isSelected;

    public Player(bool _isPlayerOne)
    {
        units = new List<Unit>();
        this.isPlayerOne = _isPlayerOne;
    }

}
