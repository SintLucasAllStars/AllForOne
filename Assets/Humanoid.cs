using System.Collections;
using System.Collections.Generic;

public class Humanoid
{

    private string name;
    private string team;
    private int health;
    private int strength;
    private int defense;
    private float speed;
    public Humanoid(string name, string team, int health, int strength, int defense, float speed)
    {
        this.name = name;
        this.team = team;
        this.health = health;
        this.strength = strength;
        this.defense = defense;
        this.speed = speed;
    }

    public string GetName()
    {
        return name;
    }
    public string GetTeam()
    {
        return team;
    }
    public int GetHealth()
    {
        return health;
    }
    public int GetStrength()
    {
        return strength;
    }
    public int GetDefense()
    {
        return defense;
    }
    public float GetSpeed()
    {
        return speed;
    }

}
