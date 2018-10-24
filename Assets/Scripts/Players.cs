using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public List<GameObject> characterList = null;
    public Vector3 NodePosition = new Vector3(0,0,0);
    public int Money = 0;
    public bool Done = false;
    public bool IsSelected = false;

    public Player(int startMoney)
    {
        Money = startMoney;
        characterList = new List<GameObject>();
    }
}
