using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass
{
    private float health;
    private float strength;
    private float speed;
    private float defence;

    public PlayerClass(float health, float strength, float speed, float defence)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defence = defence;
    }

}
