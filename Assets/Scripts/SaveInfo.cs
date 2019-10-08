using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveInfo
{

    public static void SaveAll()
    {
        
        PlayerPrefs.SetString("PLAYERNAME", GameInfo.PlayerName);
        PlayerPrefs.SetInt("STRENGTH", GameInfo.Strength);
        PlayerPrefs.SetInt("HEALTH", GameInfo.Health);
        PlayerPrefs.SetInt("SPEED", GameInfo.Speed);
        PlayerPrefs.SetInt("DEFENSE", GameInfo.Defense);
        
        //Debug.Log(GameInfo.PlayerName);
    }
}
