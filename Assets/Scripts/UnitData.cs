using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnitData : GameData
{
    private string _type = "";
    public string Type => _type;

    [SerializeField]
    private Node _position = null;
    public Node Position => _position;

    [SerializeField]
    private bool _isActive = true;
    public bool IsActive => _isActive;

    [SerializeField]
    private PlayerSide _playerSide = PlayerSide.Blu;
    public PlayerSide PlayerSide => _playerSide;

    public UnitData(string guid, Node position, string type, bool isConnected, bool isActive, PlayerSide playerSide) : base(guid, isConnected)
    {
        _guid = guid;
        _type = type;
        _position = position;
        _isConnected = isConnected;
        _isActive = isActive;
        _playerSide = playerSide;
    }

    public UnitData(UnitData other) : base(other)
    {
        _guid = other._guid;
        _type = other._type;
        _position = other._position;
        _isConnected = other._isConnected;
        _isActive = other._isActive;
        _playerSide = other._playerSide;
    }

    public void SetPosition(Node node) => _position = node;

    public void SetActive(bool active) => _isActive = active;

    public UnitData()
    { }
}