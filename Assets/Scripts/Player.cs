using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private int playerNumber;
    private int points = 100;
    private List<GameObject> units;
    void Start()
    {
        units = new List<GameObject>();
    }

    public Player(int num)
    {
        playerNumber = num;
    }
    
    void Update()
    {
        
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
}
