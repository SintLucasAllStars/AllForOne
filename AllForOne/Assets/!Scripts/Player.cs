using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public List<Unit> Units;
    public int points;
    public string name;
    public string teamColor;

    public Player(int _points, string _name, string _teamColor)
    {
        points = _points;
        name = _name;
        teamColor = _teamColor;
    }
}
