using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int health;
    private int strenght;
    private int speed;
    private int defence;

    public Player(int health, int strenght, int speed, int defence)
    {
        this.health = health;
        this.strenght = strenght;
        this.speed = speed;
        this.defence = defence;
    }
}
