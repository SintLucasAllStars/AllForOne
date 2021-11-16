using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteraction : Interactable
{
    public enum powerUpTypes
    {
        adrenaline = 0,
        rage = 1,
        timeStop = 2
    }

    public powerUpTypes currentPowerUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag($"player{GameManager.gameManager.getTurnIndex()}Owned"))
        {
            PowerUp powerInstance;
            switch (currentPowerUp)
            {
                case powerUpTypes.adrenaline:
                    powerInstance = other.gameObject.AddComponent<Adrenaline>();
                    break;
                case powerUpTypes.rage:
                    powerInstance = other.gameObject.AddComponent<Rage>();
                    break;
                case powerUpTypes.timeStop:
                    powerInstance = other.gameObject.AddComponent<TimeStop>();
                    break;
                default:
                    powerInstance = other.gameObject.AddComponent<PowerUp>();
                    break;
            }
            powerInstance.powerUpType = (int)currentPowerUp;
            other.GetComponent<UnitController>().addPowerUp(powerInstance);
            Destroy(gameObject);
        }
    }
}
