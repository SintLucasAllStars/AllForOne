﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    private List<Unit> _units = new List<Unit>();
    private List<Player> _players = new List<Player>();

    new private void Awake()
    {
        base.Awake();

        StartCoroutine(HandleMessages());
    }

    public void SpawnUnit(Node position)
    {
        SendMessageToServer(new UnitData(Guid.NewGuid().ToString(), position, "Test", true, true, Player.Instance.GameData.PlayerSide));
    }

    private IEnumerator HandleMessages()
    {
        while (true)
        {
            if (NetworkManager.Instance.Messages.Count > 0)
            {
                string message = NetworkManager.Instance.Messages.Dequeue();
                HandleMessage(message);
            }
            yield return null;
        }
    }

    public void HandleMessage(string message)
    {
        UnitData data = JsonUtility.FromJson<UnitData>(message);
        if(string.IsNullOrEmpty(data.Type))
            UpdateClients(data);
        else
            UpdateUnits(data);
    }

    public void SendMessageToServer(GameData gameData) => NetworkManager.Instance.SendMessage(new Message(gameData));

    private void UpdateUnits(UnitData gameData)
    {
        //Unit has died.
        if (!gameData.IsActive)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].GameData.Guid == gameData.Guid)
                    _players.Remove(_players[i]);
            }
            return;
        }

        //Player existed and just gets updated.
        if (DoesPlayerExist(gameData.Guid))
        {
            //Checks if any player moves.
            for (int i = 0; i < _units.Count; i++)
            {
                if (gameData.Guid == _units[i].GameData.Guid)
                {
                    _units[i].MoveTo(gameData.Position);
                    _units[i].GameData = gameData;
                }
            }
        }
        else
        {
            Debug.Log("Spawned unit: " + gameData.Type);
            Unit u = Instantiate(Resources.Load<GameObject>("Overlord")).GetComponent<Unit>();
            u.SetPosition(gameData.Position);
        }
    }
    private void UpdateClients(GameData gameData)
    {
        Debug.Log("Client joined");
        if(Player.Instance.GameData.Guid == gameData.Guid)
        {
            Player.Instance.GameData = gameData;
        }

        //On player has disconnected.
        if (!gameData.IsConnected)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].GameData.Guid == gameData.Guid)
                    _players.Remove(_players[i]);
            }
            return;
        }
    }
    private bool DoesPlayerExist(string guid)
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (guid == _players[i].GameData.Guid)
                return true;
        }
        return false;
    }
}
