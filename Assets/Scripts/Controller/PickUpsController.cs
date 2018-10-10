using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpsController : MonoBehaviour {
	public GameObject[] weapons;
	public GameObject[] powerUps;
	public Transform[] spawnPositions;
    
	public void SpawnRandomObject(){
		if(Random.Range(0,2)!=0){
			SpawnObject(weapons[Random.Range(0, weapons.Length)]);
		}else{
			SpawnObject(powerUps[Random.Range(0, powerUps.Length)]);
		}
	}
	void SpawnObject(GameObject objectToSpawn){
		Instantiate(objectToSpawn, spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity, transform);
	}
}
