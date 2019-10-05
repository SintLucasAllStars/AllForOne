using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : PowerUps
{
    private float _speedBoostMotifier = 1.5f;
    private float _soldierspeed;

    public override void UsePowerUp(Soldier soldier)
    {
        _soldier = soldier;
        _soldierspeed = _soldier._speed;
        _soldier._speed *= _speedBoostMotifier;
        Invoke("PowerupEnd", _duration);
    }

    protected override void PowerupEnd()
    {
        _soldier._speed = _soldierspeed;
        Destroy(this.gameObject);
    }
}
