public class Player
{
    public enum Color
    {
        Red, Blue
    }
    
    private int _points;
    private string _name;
    private Color _color;

    public Player(string name, Color color)
    {
        _points = 100;
        _name = name;
        _color = color;
    }

    public bool Withdraw(int amount)
    {
        if (_points < amount)
        {
            return false;
        }

        _points -= amount;
        return true;
    }

    public Color GetColor()
    {
        return _color;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetPoints()
    {
        return _points;
    }

    public int Points
    {
        get => _points;
        set => _points = value;
    }
}