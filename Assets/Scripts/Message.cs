using System;
using UnityEngine;

[Serializable]
public class Message
{
    public GameData GameData = new GameData();

    public Message(string guid, Node position, string type, bool isConnected, bool isMasterClient, bool isActive)
    {
        GameData = new GameData(guid, position, type, isConnected, isMasterClient, isActive);
    }

    public Message(GameData gamedata) => GameData = new GameData(gamedata);

    public Message(Message other) => GameData = other.GameData;

    public Message()
    { }
}