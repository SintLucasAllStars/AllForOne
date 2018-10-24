using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanPowerUps : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] powerUps;

    private void Start()
    {
        SpawnpowerUps();
    }

    public void SpawnpowerUps()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        int objectsIndex = Random.Range(0, powerUps.Length);

        for (int i = 0; i < spawnIndex; i++)
        {
            Instantiate(powerUps[objectsIndex], spawnPoints[spawnIndex].position, spawnPoints[spawnIndex].rotation);
        }
    }
}
