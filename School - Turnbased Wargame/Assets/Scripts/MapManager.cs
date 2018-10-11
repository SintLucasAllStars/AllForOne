using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour 
{
    [SerializeField] GameObject[] mapDeck;


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
