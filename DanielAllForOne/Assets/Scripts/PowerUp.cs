using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Adrenaline, Rage, TimeMachine, None
};

public class PowerUp : MonoBehaviour
{
    public PowerUpType PowerType;
    public float PowerUpDuration;
    public float PowerUpEnhancement;
}
