using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject[] powerUps;
    public GameObject[] weapons;

    public GameObject[] itemSpawns;

    void Awake()
    {
        GameObject powerUp = powerUps[Random.Range(0, 2)];
        GameObject weapon = weapons[Random.Range(0, 2)];
        Instantiate(powerUp, itemSpawns[Random.Range(0,6)].transform.position, powerUp.transform.rotation);
        Instantiate(weapon, itemSpawns[Random.Range(0, 6)].transform.position, weapon.transform.rotation);
    }
}
