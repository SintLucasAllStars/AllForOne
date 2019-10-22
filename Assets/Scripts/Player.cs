using System.Collections.Generic;

public class Player
{
    public enum Color
    {
        Red, Blue
    }
    
    private int _points;
    private string _name;
    private Color _color;
    private List<PowerUp> _powerUps;

    public Player(string name, Color color)
    {
        _points = 100;
        _name = name;
        _color = color;
        _powerUps = new List<PowerUp>();
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

    public void AddPowerUp(PowerUp powerUp)
    {
        _powerUps.Add(powerUp);
    }

    public List<PowerUp> GetValidPowerUps()
    {
        List<PowerUp> validPowerUps = new List<PowerUp>();
        foreach (PowerUp powerUp in _powerUps)
        {
            if (powerUp.IsValid())
            {
                validPowerUps.Add(powerUp);
            }
        }
        return validPowerUps;
    }

    public List<PowerUp.Type> GetActivePowerUps()
    {
        List<PowerUp.Type> activePowerUps = new List<PowerUp.Type>();
        foreach (PowerUp powerUp in _powerUps)
        {
            powerUp.Update();
            
            if (powerUp.IsActive())
            {
                activePowerUps.Add(powerUp.GetType());
            }
        }
        return activePowerUps;
    }

    public int Points
    {
        get => _points;
        set => _points = value;
    }
}