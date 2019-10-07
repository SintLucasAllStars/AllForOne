using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [SerializeField]
    private string _guid = "";
    public string Guid => _guid;
    [SerializeField]
    private Node _position = null;
    public Node Position => _position;
    [SerializeField]
    private bool _isConnected = true, _isMasterClient = false, _isActive = true;
    public bool IsConnected => _isConnected;
    public bool IsActive => _isActive;
    public bool IsMasterClient => _isMasterClient;

    public GameData(string guid, Node position, string type, bool isConnected, bool isMasterClient, bool isActive)
    {
        _guid = guid;
        _position = position;
        _isConnected = isConnected;
        _isMasterClient = isMasterClient;
        _isActive = isActive;
    }

    public GameData(GameData other)
    {
        _guid = other._guid;
        _position = other._position;
        _isConnected = other._isConnected;
        _isMasterClient = other._isMasterClient;
        _isActive = other._isActive;
    }

    public void SetPosition(Node node) => _position = node;

    public GameData()
    { }
}