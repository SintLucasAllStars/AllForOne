using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData : GameData
{
    [SerializeField]
    private Node _position = null;
    public Node Position => _position;

    [SerializeField]
    private bool _isActive = true;
    public bool IsActive => _isActive;

    public UnitData(string guid, Node position, string type, bool isConnected, bool isActive, PlayerSide playerSide) : base(type, guid, isConnected, playerSide)
    {
        _position = position;
        _isActive = isActive;
    }

    public UnitData(UnitData other) : base(other)
    {
        _position = other._position;
        _isActive = other._isActive;
    }

    public void SetPosition(Node node) => _position = node;

    public void SetActive(bool active) => _isActive = active;

    public void SetSide(PlayerSide side) => _playerSide = side;

    public UnitData()
    { }
}