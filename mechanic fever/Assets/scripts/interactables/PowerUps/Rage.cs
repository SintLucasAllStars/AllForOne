using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rage : PowerUp
{
    public override void ActivatePowerUp()
    {
        duration = 5;

        base.ActivatePowerUp();
    }
}
