using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player
{
    private int team;
    private int points;

    //The Constructor so you can make a Unit...
    public Player(int team, int points)
    {
        //Min selects the smallest of the two and Max the biggest, Own clamp function since the Math.Clamp didn't show up and this works. (clamps between 1 and 2)
        this.team = Math.Min(Math.Max(1, team), 2);
        this.points = points;
    }

    public void removePoints(int price) { points -= price; }
}
