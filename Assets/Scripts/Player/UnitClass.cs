using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitClass
{
    private float health;
    private float strength;
    private float speed;
    private float defence;

    public UnitClass(float health, float strength, float speed, float defence)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defence = defence;
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
