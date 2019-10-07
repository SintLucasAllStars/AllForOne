using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Singleton<Player>
{
    private GameData _gameData = new GameData();
    public GameData GameData
    {
        get => _gameData;
        set => _gameData = value;
    }

    private Wallet _wallet = new Wallet(100);

    private List<Unit> _units = new List<Unit>();

    private void Start()
    {
        _gameData = new GameData(new Guid().ToString(), true, PlayerSide.Blu);
        GameManager.Instance.SendMessageToServer(_gameData);
    }

    public static Color32 GetColor(PlayerSide side)
    {
        if (side == PlayerSide.Blu)
            return new Color32(0, 0, 255, 255);
        else
            return new Color32(255, 0, 0, 255);
    }
}
