using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadInfo
{
    public static void LoadAll()
    {
        GameInfo.PlayerName = PlayerPrefs.GetString("PLAYERNAME");

        GameInfo.Strength = PlayerPrefs.GetInt("STRENGTH");
        GameInfo.Health = PlayerPrefs.GetInt("HEALTH");
        GameInfo.Speed = PlayerPrefs.GetInt("SPEED");
        GameInfo.Defense = PlayerPrefs.GetInt("DEFENSE");

        Debug.Log("Player Name: " + GameInfo.PlayerName);
        Debug.Log(GameInfo.Strength + " Strength");
        Debug.Log(GameInfo.Health + " Health");
        Debug.Log(GameInfo.Speed + " Speed");
        Debug.Log(GameInfo.Defense + " Defense");
        Debug.Log("Info Loaded");
    }  

    public static void NukeAll()
    {
        GameInfo.PlayerName = ("");

        GameInfo.Strength = 0;
        GameInfo.Health = 0;
        GameInfo.Speed = 0;
        GameInfo.Defense = 0;
    }
}
