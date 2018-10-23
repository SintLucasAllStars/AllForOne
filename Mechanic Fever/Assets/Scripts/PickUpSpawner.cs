using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs;

    Vector3[] floorPosition;

    private void Start()
    {
        GameManager.instance.EndRound += CharacterSpawned;
    }

    private void CharacterSpawned()
    {
        GameManager.instance.EndRound -= CharacterSpawned;
        GameManager.instance.StartRound += Spawn;

        GameObject[] floor = GameObject.FindGameObjectsWithTag("Floor");

        floorPosition = new Vector3[floor.Length];
        for(int i = 0; i < floor.Length; i++)
        {
            floorPosition[i] = floor[i].transform.position;
        }

    }

    private void Spawn()
    {
        Instantiate(prefabs[GetRandomPoint(prefabs.Length)], floorPosition[GetRandomPoint(floorPosition.Length)] + GetRandomVector(), Quaternion.identity);
    }

    private int GetRandomPoint(int length)
    {
        return Random.Range(0, length);
    }

    private Vector3 GetRandomVector()
    {
        return new Vector3(Random.value, 1, Random.value);
    }


}
