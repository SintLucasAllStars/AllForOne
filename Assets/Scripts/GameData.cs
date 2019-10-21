using UnityEngine;

namespace MechanicFever
{
    public enum PlayerSide
    {
        Red,
        Blu
    }

    [System.Serializable]
    public class GameData
    {
        [SerializeField]
        private string _type = "";
        public string Type => _type;

        [SerializeField]
        protected string _guid = "";
        public string Guid => _guid;

        [SerializeField]
        protected bool _isConnected = true;
        public bool IsConnected => _isConnected;

        [SerializeField]
        protected PlayerSide _playerSide = PlayerSide.Blu;
        public PlayerSide PlayerSide => _playerSide;

        public GameData(string type, string guid, bool isConnected, PlayerSide playerSide)
        {
            _type = type;
            _isConnected = isConnected;
            _guid = guid;
            _playerSide = playerSide;
        }

        public GameData(GameData gameData)
        {
            _type = gameData._type;
            _isConnected = gameData._isConnected;
            _guid = gameData._guid;
            _playerSide = gameData._playerSide;
        }

        public GameData()
        { }
    }
}