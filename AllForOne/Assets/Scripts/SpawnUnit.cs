using UnityEngine;
using System.Collections.Generic;

public class SpawnUnit : MonoBehaviour
{
    public GameObject p1Unit;
    public GameObject p2Unit;
    private GameObject Unit;
    public LayerMask CanSpawn;

    public bool isRed;
    List<UnitStats> p1Units;


    public void switchTeam()
    {
        if (isRed) { isRed = false; }
        else if (!isRed) { isRed = true; }
    }

    public void spawnUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (isRed) { Unit = p1Unit; }
        else { Unit = p2Unit; }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, CanSpawn))
        {
            Debug.Log("spawned unit");
            GameObject tempunit = Instantiate(Unit, hit.point, Quaternion.identity) as GameObject;
        }
    }
}
