using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    public float health;
    public float strength;
    public float speed;
    public float defence;

    public Unit(float health, float strength, float speed, float defence)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defence = defence;
    }

    public Unit()
    {
        this.health = 1;
        this.strength = 1;
        this.speed = 1;
        this.defence = 1;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetStrength()
    {
        return strength;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetDefence()
    {
        return defence;
    }

}
