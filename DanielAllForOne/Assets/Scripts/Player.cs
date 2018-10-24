using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private List<Unit> _playerUnitList = new List<Unit>(0);

    public Player(Color teamColor)
    {
        GetTeamColor = teamColor;
    }

    public Color GetTeamColor { get; private set; }

    public void AddUnit(Unit unit)
    {
        _playerUnitList.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _playerUnitList.Remove(unit);
    }

    public int GetUnitListCount {
        get {
            return _playerUnitList.Count;
        }
    }
}
