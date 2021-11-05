using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableSpawning : MonoBehaviour
{
    public GameObject[] weaponPrefab;
    public GameObject[] powerUpPrefab;

    public Vector3 spawnOffSet;

    private NavMeshHit hit;

    public Vector3 getSpawnPosition()
    {
        float x = GameManager.gameManager.fieldSize.x / 2;
        float y = GameManager.gameManager.fieldSize.y / 2;
        Vector3 spawnPosition = new Vector3(Random.Range(-x, x + 1), 0, Random.Range(-y, y + 1));
        return spawnPosition;
    }

    public void spawnWeapon()
    {
        Instantiate(weaponPrefab[Random.Range(0, weaponPrefab.Length)], getSpawnPosition() + spawnOffSet, Quaternion.identity);
    }

    public void spawnPowerUp()
    {
        Instantiate(powerUpPrefab[Random.Range(0, powerUpPrefab.Length)], getSpawnPosition() + spawnOffSet, Quaternion.identity);
    }
}
