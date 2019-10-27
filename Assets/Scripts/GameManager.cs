using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace MechanicFever
{
    public class GameManager : Singleton<GameManager>
    {
        private List<PlayerUnit> _units = new List<PlayerUnit>();

        [SerializeField]
        private TextMeshProUGUI _teamText = null, _unitCostText = null;

        private void Start() => StartCoroutine(HandleMessages());

        public void UpdateUnit(UnitData data) => NetworkManager.Instance.SendMessage(JsonUtility.ToJson(data));

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
                        Debug.Log("Found");
                        Map.Instance.FreeNode(_units[i].GameData.Position);
                        Destroy(_units[i].gameObject);
                        _units.RemoveAt(i);
                    }
                }
                if (GetUnitCount(gameData.PlayerSide) == 0)
                {
                    NetworkManager.Instance.Close("Player won");
                    if (gameData.PlayerSide == Player.Instance.GameData.PlayerSide)
                        Notifier.Instance.ShowNotification("You lost!");
                    else
                        Notifier.Instance.ShowNotification("You won!");

                    EnableCursor(false);
                    SceneManager.Instance.LoadScene(0);
                }
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
                _units.Add(u);
            }
        }

        private void UpdateClients(GameData gameData)
        {
            if (Player.Instance.GameData.Guid == gameData.Guid)
            {
                Player.Instance.SetGameData(gameData);
                Player.Instance.PlayerUnit.SetPlayerSide(gameData.PlayerSide);
                _teamText.text = "YOUR TEAM: " + gameData.PlayerSide;
                _unitCostText.text = "UNIT PRICE: € " + Player.Instance.PlayerUnit.Price + ",-";
            }

            if(!gameData.IsConnected)
            {
                Notifier.Instance.ShowNotification("Other user disconnected!");
                NetworkManager.Instance.Close("Player leave");
                EnableCursor(false);
                SceneManager.Instance.LoadScene(0);
            }
        }

        private bool DoesItemExist(string guid)
        {
            for (int i = 0; i < _units.Count; i++)
            {
                if (guid == _units[i].GameData.Guid)
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

        public void EnableCursor(bool isEnabled)
        {
            if (isEnabled)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}