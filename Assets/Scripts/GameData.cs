using UnityEngine;

namespace MechanicFever
{
    public enum PlayerSide
    {
        Red,
        Blu
    }

    [System.Serializable]
    public class GameData : ItemData
    {
        [SerializeField]
        protected PlayerSide _playerSide = PlayerSide.Blu;
        public PlayerSide PlayerSide => _playerSide;

        public GameData(string type, string guid, bool isConnected, PlayerSide playerSide) : base(type, guid, isConnected)
        {
            _playerSide = playerSide;
        }

        public GameData(GameData gameData) : base(gameData)
        {
            _playerSide = gameData._playerSide;
        }

        public GameData() : base()
        { }
    }
}