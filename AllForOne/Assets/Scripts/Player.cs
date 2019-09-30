using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int points;
    public string name;
    public List<Actor> actors;
    public bool ready;
    public Player(int points, string name)
    {
        this.points = points;
        this.name = name;
    }

    public void removePoints(int amount)
    {
        points -= amount;
    }

    public void setReady(bool ready)
    {
        this.ready = ready;
    }
}
