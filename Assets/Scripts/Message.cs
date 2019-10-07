using System;
using UnityEngine;

[Serializable]
public class Message
{
    public UnitData GameData = new UnitData();

    public Message(string guid, Node position, string type, bool isConnected, bool isActive, PlayerSide playerSide)
    {
        GameData = new UnitData(guid, position, type, isConnected, isActive, playerSide);
    }

    public Message(UnitData gamedata) => GameData = new UnitData(gamedata);

    public Message(Message other) => GameData = other.GameData;

    public Message()
    { }
}