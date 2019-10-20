using System;
using System.Collections.Generic;
using UnityEngine;

namespace AllForOne
{
    public class Player : Singleton<Player>
    {
        private GameData _gameData = new GameData();
        public GameData GameData => _gameData;

        private Wallet _wallet = new Wallet(100);
        public Wallet Wallet => _wallet;

        private List<PlayerUnit> _units = new List<PlayerUnit>();

        [SerializeField]
        private UnitData _playerUnit;
        public UnitData PlayerUnit => _playerUnit;

        private void Start()
        {
            //_gameData = new GameData("Player", Guid.NewGuid().ToString(), true, PlayerSide.Blu);
            //GameManager.Instance.SendMessage(_gameData);
        }

        public void SetGameData(GameData gameData) => _gameData = gameData;

        public void SetPlayerUnit(UnitData gameData)
        {
            _playerUnit = new UnitData(gameData.Guid, gameData.Position, gameData.Type, gameData.IsConnected, gameData.IsActive, _gameData.PlayerSide, gameData.Health, gameData.Strength, gameData.Speed, gameData.Defense, CalculateUnitPrice(gameData.Health, gameData.Strength, gameData.Speed, gameData.Defense));
        }

        private int CalculateUnitPrice(int health, int strength, int speed, int defense)
        {
            return health + strength + speed + defense;
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