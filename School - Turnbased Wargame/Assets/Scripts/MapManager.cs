using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour 
{
    [SerializeField] List <GameObject> mapDeck = new List<GameObject>();


    private void Awake()
    {
        foreach (Transform o in GetComponentsInChildren<Transform>())
        {
            if (o.tag == "Roof")
            {
                mapDeck.Add(o.gameObject);
            }
        }
    }

    private void Start()
    {
        MapDeck(false);
    }

    public void MapDeck(bool enable)
    {
        foreach (GameObject a in mapDeck)
        {
            a.SetActive(enable);
        }
    }
}
