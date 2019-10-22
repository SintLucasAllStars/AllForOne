using System;
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
            if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
                return;

            GameManager.Instance.UpdatePowerup(new PowerupData(
                Map.Instance.GetRandomFreeNode(), 
                true, 
                "Powerup", 
                Guid.NewGuid().ToString(), 
                true, 
                (PowerupType)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PowerupType)).Length), 
                (PlayerSide)UnityEngine.Random.Range(0, Enum.GetNames(typeof(PlayerSide)).Length)));
        }
    }
}