using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public string name;
    public int damage;
    public int range;
    public int speed;

    public Weapon(string name, int damage, int range, int speed)
    {
        this.name = name;
        this.speed = speed;
        this.damage = damage;
        this.range = range;
    }

 
}
