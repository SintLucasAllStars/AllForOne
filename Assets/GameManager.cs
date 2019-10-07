using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private List<Unit> _units = new List<Unit>();
    private List<Player> _players = new List<Player>();

    new private void Awake()
    {
        base.Awake();

        StartCoroutine(HandleMessages());
    }

    private IEnumerator HandleMessages()
    {
        while (true)
        {
            if (NetworkManager.Instance.Messages.Count > 0)
            {
                Message message = NetworkManager.Instance.Messages.Dequeue();
                HandleMessage(message);
            }
            yield return null;
        }
    }

    public void HandleMessage(Message message) => UpdateClients(message.GameData);

    public void SendMessageToServer(UnitData gameData) => NetworkManager.Instance.SendMessage(new Message(gameData));

    private void UpdateClients(UnitData gameData)
    {
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
        //Player did not exist and is new.
        else if (!string.IsNullOrEmpty(gameData.Guid))
        {
            Unit p = Instantiate(Resources.Load<GameObject>("Prefabs/" + gameData/*.type*/), Map.Instance.transform).GetComponent<Unit>();

            p.GameData = gameData;

            p.SetPosition(gameData.Position);

            _units.Add(p);
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
