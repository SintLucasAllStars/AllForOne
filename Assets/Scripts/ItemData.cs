using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechanicFever
{
    public class ItemData
    {
        [SerializeField]
        protected string _type = "";
        public string Type => _type;

        [SerializeField]
        protected string _guid = "";
        public string Guid => _guid;

        [SerializeField]
        protected bool _isConnected = true;
        public bool IsConnected => _isConnected;

        public ItemData(string type, string guid, bool isConnected)
        {
            _type = type;
            _guid = guid;
            _isConnected = isConnected;
        }

        public ItemData(ItemData itemData)
        {
            _type = itemData._type;
            _guid = itemData._guid;
            _isConnected = itemData._isConnected;
        }

        public ItemData()
        { }
    }
}