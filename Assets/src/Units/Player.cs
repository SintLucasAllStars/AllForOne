using UnityEngine;

public class Player
{
    public Color Color { get; }
    public string Name { get; }
    public int Points { get; private set; } = 100;

    public Player(string name, Color color)
    {
        Color = color;
        Name = name;
    }
    
    public void SubstractPoints(int amount) => Points -= amount;
    
}