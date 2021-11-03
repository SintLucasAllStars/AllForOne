using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public TurnManager.Turns owner;

    public int health { private set; get; }
    public int strength { private set; get; }
    public int speed { private set; get; }
    private int defense;

    public bool fortified { private set; get; }

    public CharacterStats(int health, int strength, int speed, int defense, TurnManager.Turns owner)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
        this.owner = owner;
        fortified = false;
    }

    public int getDefense()
    {
        int value = 0;
        if (fortified) {value = defense;}
        return value;
    }

    public void SetFortified(bool value)
    {
        fortified = value;
    }

    public bool TakeDamage(int damageValue)
    {
        bool value = false;
        health -= damageValue;
        if (health <= 0)
        {
            value = true;
        }
        return value;
    }

    public override string ToString()
    {
        return base.ToString() + $"||health: {health}, strength: {strength}, speed: {speed}, defense: {defense}, owner: {owner}";
    }
}
