using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    GameObject[] objectsToFind;
    public string objectsToFindTag;
    void Start()
    {
        if (objectsToFind != null)
        {
            objectsToFind = GameObject.FindGameObjectsWithTag(objectsToFindTag);
            for (int i = 0; i < objectsToFind.Length; i++)
            {
                print(objectsToFind[i].name);
            }
        }
    }
    
    void Update()
    {
        
    }
}
