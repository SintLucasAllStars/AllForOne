using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int powerUpType;
    private float duration;

    public void StartPowerUp()
    {
        switch (powerUpType)
        {
            case 0:
                duration = 10;
                break;
            case 1:
                duration = 5;
                break;
            case 2:
                duration = 3;
                break;
        }

        if (powerUpType == 2) { GameManager.turnManager.turnTimerPaused = true; }

        StartCoroutine(PowerupTimer());
    }

    private IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(duration);
        if (powerUpType == 2) { GameManager.turnManager.turnTimerPaused = false; }
        Destroy(this, 0.5f);
    }

    public void CancelPowerUp()
    {
        StopAllCoroutines();
    }
}
