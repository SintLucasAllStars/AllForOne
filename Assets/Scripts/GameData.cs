using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSide
{
    Red,
    Blu
}

[System.Serializable]
public class GameData
{
    [SerializeField]
    protected string _guid = "";
    public string Guid => _guid;

    [SerializeField]
    protected bool _isConnected = true;
    public bool IsConnected => _isConnected;

    [SerializeField]
    protected PlayerSide _playerSide = PlayerSide.Blu;
    public PlayerSide PlayerSide => _playerSide;

    public GameData(string guid, bool isConnected, PlayerSide playerSide)
    {
        _isConnected = isConnected;
        _guid = guid;
        _playerSide = playerSide;
    }

    public GameData(GameData gameData)
    {
        _isConnected = gameData._isConnected;
        _guid = gameData._guid;
        _playerSide = gameData._playerSide;
    }

    public GameData()
    { }
}
