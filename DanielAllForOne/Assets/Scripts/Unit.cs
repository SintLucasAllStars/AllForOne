using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    public UnitStats UnitStats;

}

public struct UnitStats
{
    public int Health;
    public int Strenght;
    public int Speed;
    public int Defense;
}
