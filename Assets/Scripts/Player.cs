using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace MechanicFever
{
    public class Player : Singleton<Player>
    {
        private GameData _gameData = new GameData();
        public GameData GameData => _gameData;

        private Wallet _wallet = new Wallet(100);
        public Wallet Wallet => _wallet;

        [SerializeField]
        private UnitData _playerUnit;
        public UnitData PlayerUnit => _playerUnit;

        private void OnEnable() => NetworkManager.ConnectionSuccessful += Connect;

        private void OnDisable() => NetworkManager.ConnectionSuccessful -= Connect;

        private void Connect()
        {
            _gameData = new GameData("Player", Guid.NewGuid().ToString(), true, PlayerSide.Red);
            NetworkManager.Instance.SendMessage(JsonUtility.ToJson(_gameData));
        }

        public void SetGameData(GameData gameData) => _gameData = gameData;

        public void SetPlayerUnit(UnitData gameData)
        {
            _playerUnit = gameData;
        }

        public static Color32 GetColor(PlayerSide side)
        {
            if (side == PlayerSide.Blu)
                return new Color32(0, 0, 255, 255);
            else
                return new Color32(255, 0, 0, 255);
        }
    }
}