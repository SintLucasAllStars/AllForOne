using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : Singelton<MapManager>
{

    [SerializeField]
    private GameObject itemPrefab;

    private List<GameObject> mapTiles;

    private void Start()
    {
        mapTiles = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            mapTiles.Add(transform.GetChild(i).gameObject);
        }
    }

    public void SpawnRandomItem()
    {
        Vector3 pos = mapTiles[Random.Range(0, mapTiles.Count)].transform.position;
        GameObject item = Instantiate(itemPrefab, pos + new Vector3(0, 0.2f, 0), Quaternion.identity);
        item.GetComponent<PickupItem>().GetRandomItem();


        pos += new Vector3(0, 0.1f, 0);
    }
}
