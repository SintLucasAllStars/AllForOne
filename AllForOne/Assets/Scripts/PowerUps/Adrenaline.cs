using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : PowerUp
{
    private float _buffValue = 1.5f;

    public override IEnumerator Use(Unit unit)
    {
        unit._speed *= _buffValue;
        yield return new WaitForSeconds(_time);
        unit._speed /= _buffValue;
    }
}
