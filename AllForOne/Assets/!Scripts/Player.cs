using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    List<Unit> Units;
    public int points;

    public Player(int _points)
    {
        points = _points;
    }
}
