using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private int health;
    private int speed;
    private int strength;
    private int defense;
    
    public Unit(int h, int sp, int st, int d)
    {
        health = h;
        speed = sp;
        strength = st;
        defense = d;
    }

    public int GetHealth()
    {
        return health;
    }
    
    public int GetSpeed()
    {
        return speed;
    }
    
    public int GetStrength()
    {
        return strength;
    }
    
    public int GetDefense()
    {
        return defense;
    }
}
