using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PowerUp", menuName = "PowerUps")]
public class PowerUp : ScriptableObject
{
    public Power power;
}

public enum Power { Adrenaline, Rage, TimeMachine };