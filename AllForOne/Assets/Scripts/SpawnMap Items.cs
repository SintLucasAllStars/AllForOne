using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMapItems : MonoBehaviour
{
    public GameObject[] items;
    public Transform[] spawnplaces;

    private void SpawnItems()
    {
        Instantiate(items[Random.Range(0, items.Length)], spawnplaces[Random.Range(0, spawnplaces.Length)].position, Quaternion.identity);
    }
}
