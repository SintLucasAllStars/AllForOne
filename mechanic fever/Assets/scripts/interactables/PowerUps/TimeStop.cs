using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : PowerUp
{
    public override void ActivatePowerUp()
    {
        duration = 3;
        GameManager.gameManager.turnTimerPaused = true;

        base.ActivatePowerUp();
    }

    protected override void DestroyPowerup()
    {
        GameManager.gameManager.turnTimerPaused = false;
        base.DestroyPowerup();
    }
}
