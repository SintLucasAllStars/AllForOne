using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerName;
    public Color playerColor;

    public List<GameObject> playerGameObject = new List<GameObject>();

    public Character this[int index] 
    {
        get
        {
            return playerGameObject[index].GetComponent<Character>();
        }
    }

    public Player (Color pColor)
    {
        this.playerName = "NewPlayer";
        this.playerColor = pColor;
    }
}
