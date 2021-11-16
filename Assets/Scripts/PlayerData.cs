using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    private bool _red;
    
    private int _points;
    private List<UnitData> _UD = new List<UnitData>();
    private bool _donePlacing = false;
    
    public PlayerData(bool red)
    {
        _red = red;
        _points = 100;
    }

    public bool red
    {
        get { return _red; }
    }

    public bool hasUnits()
    {
        return _UD.Count > 0;
    }

    public bool AddUnit(UnitData UD)
    {
        _UD.Add(UD);
        return _UD.Exists(unit => unit == UD);
    }

    public bool RemoveUnit(UnitData UD)
    {
        _UD.Remove(UD);
        return !_UD.Exists(unit => unit == UD);
    }

    public int Points
    {
        get { return _points; }
        set { _points = value; }
    }

    public bool FindUnit(UnitData unit)
    {
        return _UD.Exists(PlayerUnit => PlayerUnit == unit);
    }

    public bool DonePlacing
    {
        get { return _donePlacing; }
        set { if (!_donePlacing && value) _donePlacing = value; }
    }
}
