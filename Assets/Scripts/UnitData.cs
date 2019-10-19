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

    [SerializeField]
    private int _health;
    public int Health => _health;

    public UnitData(string guid, Node position, string type, bool isConnected, bool isActive, PlayerSide playerSide, int health) : base(type, guid, isConnected, playerSide)
    {
        _position = position;
        _isActive = isActive;
        _health = health;
    }

    public UnitData(UnitData other) : base(other)
    {
        _position = other._position;
        _isActive = other._isActive;
        _health = other._health;
    }

    public void SetPosition(Node node) => _position = node;

    public void SetActive(bool active) => _isActive = active;

    public void SetSide(PlayerSide side) => _playerSide = side;

    public void SetHealth(int health) => _health = health;

    public UnitData()
    { }
}