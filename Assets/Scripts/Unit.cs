using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Unit
{
    [SerializeField]
    int owner;

    [SerializeField]
    int health;
    [SerializeField]
    int strength;
    [SerializeField]
    int speed;
    [SerializeField]
    int defense;

    public Unit(int ownedByPlayer, int health, int strength, int speed, int defense)
    {
        this.owner = ownedByPlayer;
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }

    public int GetHealth()
    {
        return this.health;
    }

    public void SetStrength(int strength)
    {
        this.strength = strength;
    }

    public int GetStrength()
    {
        return this.strength;
    }

    public void SetSpeed(int speed)
    {
        this.speed = speed;
    }

    public int GetSpeed()
    {
        return this.speed;
    }

    public void SetDefense(int defense)
    {
        this.defense = defense;
    }

    public int GetDefense()
    {
        return this.defense;
    }

    public int GetOwner()
    {
        return owner;
    }
}
