using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    public GameObject standardCharacterPrefab;

    private GameObject characterToBuild;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("there are way to many");
        }
        instance = this;
    }

    private void Start()
    {
        characterToBuild = standardCharacterPrefab;
    }

    public GameObject GetCharacterToBuild()
    {
        return characterToBuild;
    }
}
