using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private int costs;
    private float health;
    private float strength;
    private float speed;
    private float defence;
    private Color unitTeamColor;

    public void Initialize(string UnitType)
    {
        switch (UnitType)
        {
            case "WeakUnit":
                health = 20;
                strength = 25;
                speed = 5;
                defence = 25;
                costs = 10;
                break;

            case "StrongUnit":
                health = 100;
                strength = 75;
                speed = 20;
                defence = 50;
                costs = 25;
                break;

            default:
                break;
        }
    }

    public int GetProductionCosts()
    {
        return costs;
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

    public Color SetUnitTeamColor
    {
        get
        {
            return unitTeamColor;
        }
        set
        {
            unitTeamColor = value;
        }
    }

    public Color GetUnitTeamColor()
    {
        return unitTeamColor;
    }

}
