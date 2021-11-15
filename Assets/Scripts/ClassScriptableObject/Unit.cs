using System;

public class Unit
{
    private bool placed;
    private int team;
    private string name;
    private int health, strength, speed, defense;
    private bool isDefending;

    //The Constructor so you can make a Unit...
    public Unit(int team, string name, int health, int strength, int speed, int defense)
    {
        //Min selects the smallest of the two and Max the biggest, Own clamp function since the Math.Clamp didn't show up and this works. (clamps between 1 and 2)
        this.team = Math.Min(Math.Max(1, team), 2);
        this.name = name;
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
        placed = false;

        if (health + strength + speed + defense > 100)
        {
            this.health = 0;
        }
    }

    //Exmp: Only able to get health, not set
    public int getHealth
    {
        get { return health; }
        //protected set { }
    }
    
    //Exmp: Written out more, because why not
    public int Team
    {
        get { return this.team; }
        set { this.team = value; }
    }

    public int AddDamage(int DamageToAdd = 0)
    {
        //Make it always positive so it doesn't matter what I fill in the calculation will do the damage.
        DamageToAdd = Math.Abs(DamageToAdd);
        //If the unit is fortified and the defense is on:
        if (isDefending)
        {
            if (defense - DamageToAdd < 0) health += defense - DamageToAdd;
        }

        return health;
    }

    public bool isPlaced()
    {
        return placed;
    }

    public bool Fortified(bool fortified)
    {
        isDefending = fortified;
        //Debugging purposes just in case
        return isDefending;
    }
}
