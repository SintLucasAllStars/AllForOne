using UnityEngine;

namespace MechanicFever
{
    [System.Serializable]
    public class UnitData : ItemData
    {
        [SerializeField]
        private int _health;
        public int Health => _health;

        [SerializeField]
        protected int _strength, _speed, _defense, _price;
        public int Strength => _strength;
        public int Speed => _speed;
        public int Defense => _defense;
        public int Price => _price;

        public UnitData(int health, int strength, int speed, int defense, int price, string guid, Node position, string type, bool isConnected, bool isActive, PlayerSide playerSide) : base(position, isActive, type, guid, isConnected, playerSide)
        {
            _health = health;
            _strength = strength;
            _speed = speed;
            _defense = defense;
            _price = price;
        }

        public UnitData(UnitData other) : base(other)
        {
            _health = other._health;
            _strength = other._strength;
            _speed = other._speed;
            _defense = other._defense;
            _price = other._price;
        }

        public void SetSide(PlayerSide side) => _playerSide = side;

        public void SetHealth(int health) => _health = health;

        public void SetPrice(int price) => _price = price;

        public UnitData()
        { }
    }
}