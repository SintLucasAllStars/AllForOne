using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : PowerUp
{
    private float _buffValue = 1.1f;

    public override IEnumerator Use(Unit unit)
    {
        unit._strenght *= _buffValue;
        yield return new WaitForSeconds(_time);
        unit._strenght /= _buffValue;
    }
}
