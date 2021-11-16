using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnUnit spawner;
    private bool canSpawn = true;
    private int unitsSpawned;
    public Camera mainCam;

    RaycastHit hit2;

    void Update()
    {
        //test system
        if (canSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //spawn unit swap team at 6 stop spawning
                spawner.spawnUnit();
                spawner.switchTeam();
                unitsSpawned++;
                if (unitsSpawned == 6) { canSpawn = false; }
            }
        }

        if (!canSpawn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit2, Mathf.Infinity))
                {
                    if (hit2.transform.CompareTag("p1"))
                    {
                        hit2.transform.gameObject.GetComponentInChildren<Movement>().enabled = false;
                        mainCam.enabled = false;
                    }

                    if (hit2.transform.CompareTag("p2"))
                    {
                        hit2.transform.gameObject.GetComponentInChildren<Movement>().enabled = false;
                        mainCam.enabled = false;
                    }
                }
            }
        }
    }
}
