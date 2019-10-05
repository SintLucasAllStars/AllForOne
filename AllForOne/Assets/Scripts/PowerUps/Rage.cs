using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : PowerUps
{
    private float _damageBoostMotifier = 1.1f;
    private float _soldierStrength;

    public override void UsePowerUp(Soldier soldier)
    {
        _soldier = soldier;
        _soldierStrength = _soldier._strenght;
        _soldier._strenght *= _damageBoostMotifier;
        Invoke("PowerupEnd", _duration);
    }

    protected override void PowerupEnd()
    {
        _soldier._strenght = _soldierStrength;
        Destroy(this.gameObject);
    }
}
