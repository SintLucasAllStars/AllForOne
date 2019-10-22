using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement1 : MonoBehaviour
{
    public static int Strength, Health, Speed, Defence;

    public static void Save()
    {
        Debug.Log("Saved1");
        Strength = Unit2.Strength;
        Health = Unit2.Health;
        Speed = Unit2.Speed;
        Defence = Unit2.Defence;
        
        SavePlayerInfo.SaveAll1();
    }
}
