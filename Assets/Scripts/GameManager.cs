using System.Collections;
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

    public void SpawnUnit(UnitData data)
    {
        SendMessage(data);
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
        if (string.Equals(data.Type, "Player"))
            UpdateClients(data);
        else if (string.Equals(data.Type, "Turn"))
            TurnManager.Instance.SetTurn(data.PlayerSide);
        else
            UpdateUnits(data);
    }

    public void SendMessage(GameData gameData) => NetworkManager.Instance.SendMessage(new Message(gameData));

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
                    _units[i].SetGameData(gameData);
                }
            }
        }
        else
        {
            Debug.Log("Spawned unit: " + gameData.Type);
            Unit u = Instantiate(Resources.Load<GameObject>(gameData.Type)).GetComponent<Unit>();
            u.SetGameData(gameData);
        }
    }
    private void UpdateClients(GameData gameData)
    {
        Debug.Log("Client joined");
        if(Player.Instance.GameData.Guid == gameData.Guid)
        {
            Player.Instance.SetGameData(gameData);
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
