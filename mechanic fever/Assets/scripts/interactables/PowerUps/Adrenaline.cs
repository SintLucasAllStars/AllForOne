using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : PowerUp
{
    public override void ActivatePowerUp()
    {
        duration = 10;

        base.ActivatePowerUp();
    }
}
