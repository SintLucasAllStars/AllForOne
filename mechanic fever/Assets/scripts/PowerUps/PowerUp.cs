using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum powerUpTypes
    {
        adrenaline = 0,
        rage = 1,
        timeStop = 2
    }

    public powerUpTypes currentPowerUp;
    private float duration;
    private CharacterStats bonusStats;

    public void StartPowerUp()
    {
        switch ((int)currentPowerUp)
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

        StartCoroutine(PowerupTimer());
    }

    private IEnumerator PowerupTimer()
    {
        print(currentPowerUp);
        yield return new WaitForSeconds(duration);
        Destroy(this);
        print("end powerUp");
    }
}
