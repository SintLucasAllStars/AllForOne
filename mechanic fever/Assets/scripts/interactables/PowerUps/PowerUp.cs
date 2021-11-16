using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int powerUpType;
    protected float duration = 0;

    public virtual void ActivatePowerUp()
    {
        StartCoroutine(PowerupTimer());
    }

    private IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(duration);
        DestroyPowerup();
    }

    protected virtual void DestroyPowerup()
    {
        Destroy(this, 0.5f);
    }

    public void CancelPowerUp()
    {
        StopAllCoroutines();
    }
}
