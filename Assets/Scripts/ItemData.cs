using UnityEngine;

namespace MechanicFever
{
    public class ItemData : GameData
    {
        [SerializeField]
        private Node _position = null;
        public Node Position => _position;

        public void SetPosition(Node node) => _position = node;

        [SerializeField]
        private bool _isActive = true;
        public bool IsActive => _isActive;

        public void SetActive(bool active) => _isActive = active;

        public ItemData(Node position, bool isActive, string type, string guid, bool isConnected, PlayerSide playerSide) : base(type, guid, isConnected, playerSide)
        {
            _position = position;
            _isActive = isActive;
        }

        public ItemData(ItemData itemData) : base(itemData)
        {
            _isActive = itemData._isActive;
            _type = itemData._type;
            _guid = itemData._guid;
            _isConnected = itemData._isConnected;
        }

        public ItemData()
        { }
    }
}