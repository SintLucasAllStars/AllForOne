using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : PowerUp
{
    public override IEnumerator Use(Unit unit)
    {
        unit._freeze = true;
        yield return new WaitForSeconds(_time);
        unit._freeze = false;
    }
}
