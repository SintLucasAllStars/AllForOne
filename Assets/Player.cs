using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSide
{
    Red,
    Blu
}

public class Player : Singleton<Player>
{
    private UnitData _gameData = new UnitData();
    public UnitData GameData => _gameData;

    private Wallet _wallet = new Wallet(100);

    private List<Unit> _units = new List<Unit>();

    public static Color32 GetColor(PlayerSide side)
    {
        if (side == PlayerSide.Blu)
            return new Color32(0, 0, 255, 255);
        else
            return new Color32(255, 0, 0, 255);
    }
}
