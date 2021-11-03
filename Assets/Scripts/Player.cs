using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    float health;
    float strength;
    float speed;
    float defense;

    public Player(float health, float strength, float speed, float defense)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
    }

    public void SetHealth(float health)
    {
        this.health = health;
    }

    public float GetHealth()
    {
        return this.health;
    }

    public void SetStrength(float strength)
    {
        this.strength = strength;
    }

    public float GetStrength()
    {
        return this.strength;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public float GetSpeed()
    {
        return this.speed;
    }

    public void SetDefense(float defense)
    {
        this.defense = defense;
    }

    public float GetDefense()
    {
        return this.defense;
    }
}
