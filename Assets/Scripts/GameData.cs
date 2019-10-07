using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    [SerializeField]
    protected string _guid = "";
    public string Guid => _guid;

    [SerializeField]
    protected bool _isConnected = true;
    public bool IsConnected => _isConnected;

    public GameData(string guid, bool isConnected)
    {
        _isConnected = isConnected;
        _guid = guid;
    }

    public GameData(GameData gameData)
    {
        _isConnected = gameData._isConnected;
        _guid = gameData._guid;
    }

    public GameData()
    { }
}
