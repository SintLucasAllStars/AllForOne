using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private int health;
    private int strenght;
    private int speed;
    private int defence;

    public Unit(int health, int strenght, int speed, int defence)
    {
        this.health = health;
        this.strenght = strenght;
        this.speed = speed;
        this.defence = defence;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetStrenght()
    {
        return strenght;
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
