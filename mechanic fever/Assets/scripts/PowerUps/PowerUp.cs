using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public int powerUpType;
    private float duration;

    public void Awake()
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
    }

    public void StartPowerUp()
    {
        StartCoroutine(PowerupTimer());
    }

    private IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(duration);
        Destroy(this);
    }
}
