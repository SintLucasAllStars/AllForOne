using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechanicFever
{
    public class GameManager : Singleton<GameManager>
    {
        private List<PlayerUnit> _units = new List<PlayerUnit>();
        private List<Player> _players = new List<Player>();

        public int Connections => _players.Count;

        private void Start()
        {
            StartCoroutine(HandleMessages());
        }

        public void SpawnUnit(UnitData data) => NetworkManager.Instance.SendMessage(data);

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

            //Unit existed and just gets updated.
            if (DoesUnitExist(gameData.Guid))
            {
                //Checks if any player moves.
                for (int i = 0; i < _units.Count; i++)
                {
                    if (gameData.Guid == _units[i].GameData.Guid)
                    {
                        _units[i].MoveTo(gameData.Position);
                        _units[i].SetGameData(gameData);
                        _units[i].SetPosition(gameData.Position);
                    }
                }
            }
            else //Unit did not exist and just got purchased.
            {
                PlayerUnit u = Instantiate(Resources.Load<GameObject>(gameData.Type), this.transform).GetComponent<PlayerUnit>();
                u.SetGameData(gameData);
                u.SetPosition(gameData.Position);
            }
        }

        private void UpdateClients(GameData gameData)
        {
            if (Player.Instance.GameData.Guid == gameData.Guid)
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

        private bool DoesUnitExist(string guid)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (guid == _units[i].GameData.Guid)
                    return true;
            }
            return false;
        }
    }
}