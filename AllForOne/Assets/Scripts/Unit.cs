using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private int health;
    private int strength;
    private int speed;
    private int defence;

    public Unit(int health, int strength, int speed, int defence)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defence = defence;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetStrength()
    {
        return strength;
    }

    public int GetSpeed()
    {
        return speed;
    }

    public int GetDefence()
    {
        return defence;
    }
}
