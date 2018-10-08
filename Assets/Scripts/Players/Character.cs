using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public int Health;
    public int Speed;
    public int Strength;
    public int Defence;

    public Character(int strength, int defence, int speed, int health)
    {
        Strength = strength;
        Defence = defence;
        Speed = speed;
        Health = health;
    }

}
