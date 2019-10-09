using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    List<Unit> Units;
    public int points;
    public string name;

    public Player(int _points, string _name)
    {
        points = _points;
        name = _name;
    }
}
