using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string playerName;
    public ushort playerMoney;
    public Color playerColor;
    public Color playerUIColor;


    public List<GameObject> playerGameObject = new List<GameObject>();
    public Character this[int index] 
    {
        get
        {
            return playerGameObject[index].GetComponent<Character>();
        }
    }

    public Player (Color pColor, Color pUIColor)
    {
        this.playerName = "NewPlayer";
        this.playerMoney = 100;
        this.playerColor = pColor;
        this.playerUIColor = pUIColor;
    }
}
