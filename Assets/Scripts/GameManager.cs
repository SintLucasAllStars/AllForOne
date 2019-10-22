using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechanicFever
{
    public class GameManager : Singleton<GameManager>
    {
        private List<PlayerUnit> _units = new List<PlayerUnit>();
        private List<Player> _players = new List<Player>();
        private List<Powerup> _powerups = new List<Powerup>();

        public int Connections => _players.Count;

        private void Start() => StartCoroutine(HandleMessages());

        public void SpawnUnit(UnitData data) => NetworkManager.Instance.SendMessage(JsonUtility.ToJson(data));

        public void UpdatePowerup(PowerupData data) => NetworkManager.Instance.SendMessage(JsonUtility.ToJson(data));

        public void ChangeTurn(GameData data) => NetworkManager.Instance.SendMessage(JsonUtility.ToJson(data));

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
            GameData data = JsonUtility.FromJson<ItemData>(message);
            switch (data.Type)
            {
                case "Player":
                    UpdateClients(data);
                    break;
                case "Powerup":
                    PowerupData powerup = JsonUtility.FromJson<PowerupData>(message);
                    UpdatePowerups(powerup);
                    break;
                case "Turn":
                    TurnManager.Instance.SetTurn(data.PlayerSide);
                    break;
                default:
                    UnitData unit = JsonUtility.FromJson<UnitData>(message);
                    UpdateUnits(unit);
                    break;
            }
        }

        private void UpdateUnits(UnitData gameData)
        {
            //Unit has died.
            if (!gameData.IsActive)
            {
                for (int i = 0; i < _units.Count; i++)
                {
                    if (_units[i].GameData.Guid == gameData.Guid)
                    {
                        Map.Instance.FreeNode(_units[i].GameData.Position);
                        Destroy(_units[i].gameObject);
                        _units.RemoveAt(i);
                    }
                }
                if (GetUnitCount(gameData.PlayerSide) == 0)
                    Debug.Log(gameData.PlayerSide + " player loses.");
                return;
            }

            //Unit existed and just gets updated.
            if (DoesItemExist(gameData.Guid))
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
            else //Unit did not exist and just got purchased.
            {
                PlayerUnit u = Instantiate(Resources.Load<GameObject>(gameData.Type)).GetComponent<PlayerUnit>();
                u.SetGameData(gameData);
                u.SetPosition(gameData.Position);
            }
        }

        private void UpdatePowerups(PowerupData gameData)
        {
            //Powerup has been picked up.
            if (!gameData.IsActive)
            {
                for (int i = 0; i < _powerups.Count; i++)
                {
                    if (_powerups[i].PowerupData.Guid == gameData.Guid)
                    {
                        Map.Instance.FreeNode(_powerups[i].PowerupData.Position);
                        Destroy(_powerups[i].gameObject);
                        _powerups.RemoveAt(i);
                    }
                }
                return;
            }

            //Powerup gets spawned.
            if (!DoesItemExist(gameData.Guid))
            {
                Powerup u = Instantiate(Resources.Load<GameObject>(gameData.Type)).GetComponent<Powerup>();
                u.SetPowerupData(gameData);
                u.SetPosition(gameData.Position);
                _powerups.Add(u);
            }
        }

        private void UpdateClients(GameData gameData)
        {
            if (Player.Instance.GameData.Guid == gameData.Guid)
            {
                Player.Instance.SetGameData(gameData);
                Player.Instance.PlayerUnit.SetPlayerSide(gameData.PlayerSide);
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

        private bool DoesItemExist(string guid)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (guid == _units[i].GameData.Guid)
                    return true;
            }
            for (int i = 0; i < _powerups.Count; i++)
            {
                if (guid == _powerups[i].PowerupData.Guid)
                    return true;
            }
            return false;
        }

        private int GetUnitCount(PlayerSide playerSide)
        {
            int numUnits = 0;
            for (int i = 0; i < _units.Count; i++)
            {
                if (_units[i].GameData.PlayerSide == playerSide)
                    numUnits++;
            }
            return numUnits;
        }
    }
}