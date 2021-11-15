using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnUnit spawner;

    //p1 spawns unit
    //p2 spawns unit


    //p1 clicks and walks unit
    //p2 clicks and walks unit

    //p1 clicks loop again

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            spawner.spawnUnit();
            spawner.switchTeam();
        }
    }
}
