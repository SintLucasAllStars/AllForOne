using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : Singleton<TurnManager>
{
    private PlayerSide _currentTurn;
    public PlayerSide CurrentTurn => _currentTurn;

    public void NextTurn()
    {
        int i = ((int)_currentTurn + 1) % Enum.GetNames(typeof(PlayerSide)).Length;
        Debug.Log(i);
        _currentTurn = (PlayerSide)i;
        GameManager.Instance.SendMessage(new GameData("Turn", Player.Instance.GameData.Guid, Player.Instance.GameData.IsConnected, _currentTurn));
    }

    public bool HasTurn(PlayerSide side) => side == _currentTurn;

    public void SetTurn(PlayerSide playerSide) => _currentTurn = playerSide;
}
