using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ItemSpawner : MonoBehaviour
{
	public GameObject[] items;

	Vector3 GetRandomPosition(float Radius)
	{
		Vector3 randomDirection = Random.insideUnitSphere * Radius;
		randomDirection += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition(randomDirection, out hit, Radius, 1);
		return hit.position;
	}

	public void SpawnItem()
	{
		Instantiate(items[Random.Range(0, items.Length)], GetRandomPosition(40) + new Vector3(0,2,0), Quaternion.identity);
	}
}
