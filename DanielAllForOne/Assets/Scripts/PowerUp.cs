using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    None, Adrenaline, Rage, TimeMachine
};

public class PowerUp : MonoBehaviour
{
    public PowerUpInfo PowerUpInfo;
}

[System.Serializable]
public struct PowerUpInfo
{
    public PowerUpType PowerType;
    public float PowerUpDuration;
    public float PowerUpEnhancement;
}
