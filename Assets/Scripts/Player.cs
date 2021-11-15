using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int playerNumber;
    private int points = 100;
    private List<GameObject> units = new List<GameObject>();

    public Player(int num)
    {
        playerNumber = num;
    }
    
    public int GetPoints()
    {
        return points;
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }

    public void SubtractPoints(int p)
    {
        points -= p;
    }

    public List<GameObject> GetList()
    {
        return units;
    }

    public void AddUnit(GameObject unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit(GameObject unit)
    {
        units.Remove(unit);
    }
}
