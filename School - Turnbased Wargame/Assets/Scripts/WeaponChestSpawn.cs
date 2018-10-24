using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChestSpawn : MonoBehaviour
{
    public List<GameObject> allWeaponChest = new List<GameObject>();
    public List<Vector3> spawnLocation = new List<Vector3>();

	public void RandomSpawnChest ()
    {
        Instantiate(allWeaponChest[Random.Range(0, allWeaponChest.Count)], spawnLocation[Random.Range(0, spawnLocation.Count)], Quaternion.identity);
    }
}
