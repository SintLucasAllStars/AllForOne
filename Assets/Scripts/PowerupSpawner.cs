using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MechanicFever
{
    public class PowerupSpawner : MonoBehaviour
    {
        private void OnEnable() => TurnManager.ChangeTurn += SpawnPowerup;

        private void OnDisable() => TurnManager.ChangeTurn -= SpawnPowerup;

        private void SpawnPowerup()
        {
            Debug.Log("Spawned powerup");
        }
    }
}