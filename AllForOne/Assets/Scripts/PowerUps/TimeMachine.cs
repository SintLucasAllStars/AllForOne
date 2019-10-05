using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : PowerUps
{
    public override void UsePowerUp(Soldier soldier)
    {
        _soldier = soldier;
        _soldier._timeFreeze = true;
        Invoke("PowerupEnd", _duration);
    }

    protected override void PowerupEnd()
    {
        _soldier._timeFreeze = false;
        Destroy(this.gameObject);
    }
}
