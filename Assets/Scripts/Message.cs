using UnityEngine;

namespace MechanicFever
{
    [System.Serializable]
    public class Message
    {
        public string GameData = "";

        public Message(GameData gamedata) => GameData = JsonUtility.ToJson(gamedata);

        public Message(Message other) => GameData = other.GameData;

        public Message()
        { }
    }
}