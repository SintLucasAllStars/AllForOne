using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float timer;
    public float value;
    public float minAngle;
    public float maxAngle;

    public GameObject enemyPrefab;

    private void Start()
    {
        timer = value;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Instantiate(enemyPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle)));

            timer = value;
        }
    }
}
