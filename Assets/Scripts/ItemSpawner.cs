using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    public float _xMin;
    public float _xMax;
    public float _zMin;
    public float _zMax;

    public float _height;

    public void SpawnPowerUp()
    {
        Vector3 pos = new Vector3(Random.Range(_xMin, _xMax), _height, Random.Range(_zMin, _zMax));
        Instantiate(weapons[Random.Range(0, weapons.Count)], pos, Quaternion.identity);
    }
}
