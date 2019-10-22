using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerInfo : MonoBehaviour
{
    public static void SaveAll1()
    {
        PlayerPrefs.SetInt("STRENGTH", UnitPlacement1.Strength);
        PlayerPrefs.SetInt("HEALTH", UnitPlacement1.Health);
        PlayerPrefs.SetInt("SPEED", UnitPlacement1.Speed);
        PlayerPrefs.SetInt("DEFENSE", UnitPlacement1.Defence);

    }
    public static void SaveAll2()
    {
        PlayerPrefs.SetInt("STRENGTH", UnitPlacement2.Strength);
        PlayerPrefs.SetInt("HEALTH", UnitPlacement2.Health);
        PlayerPrefs.SetInt("SPEED", UnitPlacement2.Speed);
        PlayerPrefs.SetInt("DEFENSE", UnitPlacement2.Defence);
      
    }
}
