using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player
{
    private int team;
    private int points;
    private Color col;
    private bool done;

    //The Constructor so you can make a Unit...
    public Player(int team, int points, Color col)
    {
        done = false;
        //Min selects the smallest of the two and Max the biggest, Own clamp function since the Math.Clamp didn't show up and this works. (clamps between 1 and 2)
        this.team = Math.Min(Math.Max(1, team), 2);
        this.points = points;
        this.col = col;
    }

    public void SetDone(bool yes) { done = yes;}
    public bool isDone()
    {
        return done;
    }
    public Color getColor() { return col; }
    public int getPoints() { return points; }
    public void removePoints(int price) { points -= price; }
}
