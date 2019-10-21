using UnityEngine;

namespace MechanicFever
{
    [System.Serializable]
    public class UnitData : GameData
    {
        [SerializeField]
        private Node _position = null;
        public Node Position => _position;

        [SerializeField]
        private bool _isActive = true;
        public bool IsActive => _isActive;

        [SerializeField]
        private int _health;
        public int Health => _health;

        [SerializeField]
        protected int _strength, _speed, _defense, _price;
        public int Strength => _strength;
        public int Speed => _speed;
        public int Defense => _defense;
        public int Price => _price;

        public UnitData(string guid, Node position, string type, bool isConnected, bool isActive, PlayerSide playerSide, int health, int strength, int speed, int defense, int price) : base(type, guid, isConnected, playerSide)
        {
            _position = position;
            _isActive = isActive;
            _health = health;
            _strength = strength;
            _speed = speed;
            _defense = defense;
            _price = price;
        }

        public UnitData(UnitData other) : base(other)
        {
            _position = other._position;
            _isActive = other._isActive;
            _health = other._health;
            _strength = other._strength;
            _speed = other._speed;
            _defense = other._defense;
            _price = other._price;
        }

        public void SetPosition(Node node) => _position = node;

        public void SetActive(bool active) => _isActive = active;

        public void SetSide(PlayerSide side) => _playerSide = side;

        public void SetHealth(int health) => _health = health;

        public void SetPrice(int price) => _price = price;

        public UnitData()
        { }
    }
}