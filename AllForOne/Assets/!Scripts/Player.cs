using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public List<Unit> Units;
    public int points;
    public string name;
    public string teamColor;
    public Sprite characterImg;

    public Player(int _points, string _name, string _teamColor, Sprite _characterImg)
    {
        points = _points;
        name = _name;
        teamColor = _teamColor;
        characterImg = _characterImg;
    }

    public void RemoveUnit(Unit unit)
    {
        Units.Remove(unit);
    }

    public void AddUnit(Unit unit)
    {
        Units.Add(unit);
    }
}
