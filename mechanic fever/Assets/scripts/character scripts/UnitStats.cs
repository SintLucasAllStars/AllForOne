using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats
{
    public string owner;
    public int ownerNumber;

    public float health { private set; get; }
    public float strength { private set; get; }
    public float speed { private set; get; }
    private float defense;

    public bool fortified { private set; get; }

    public UnitStats(float health, float strength, float speed, float defense, int ownerIndex)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
        ownerNumber = ownerIndex;
        owner = $"player{ownerIndex}";
        fortified = false;
    }

    public float getDefense()
    {
        float value = 1;
        if (fortified) {value = defense;}
        return value;
    }

    public void SetFortified(bool value)
    {
        fortified = value;
    }

    public bool TakeDamage(float  damageValue)
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
