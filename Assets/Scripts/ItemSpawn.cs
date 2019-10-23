using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject[] powerUps;
    public GameObject[] weapons;

    public GameObject[] itemSpawns;

    GameObject sword;



    void Awake()
    {
        GameObject weapon = weapons[0];
        sword = Instantiate(weapon, itemSpawns[Random.Range(0, 6)].transform.position, weapon.transform.rotation);
    }

    public void RespawnWeapons()
    {
        Destroy(sword);
        GameObject weapon = weapons[0];
        sword = Instantiate(weapon, itemSpawns[Random.Range(0, 6)].transform.position, weapon.transform.rotation);
    }
}
