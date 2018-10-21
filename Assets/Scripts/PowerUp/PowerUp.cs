using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PowerUp 
{
    public enum PowerUpEnum {Adrenaline, Rage, TimeMachine}

    public PowerUpEnum MyPowerUp;

    public float SpeedBoost;
    public float StrengthBoost;
    public bool FreezeTime;

    public float TimeLength;
    
    
    
  
}
            