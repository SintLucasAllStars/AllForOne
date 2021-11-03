using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInteraction : MonoBehaviour
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
        if (other.CompareTag($"{TurnManager.turnManager.GetCurrentTurn()}Owned"))
        {
            PowerUp powerInstance = other.gameObject.AddComponent<PowerUp>();
            powerInstance.powerUpType = (int)currentPowerUp;
            other.GetComponent<CharacterController>().addPowerUp(powerInstance);
            Destroy(gameObject);
        }
    }
}
