using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceUnit : MonoBehaviour
{
    public GameObject Unit;
    public LayerMask layer;

    private Ray _ray;
    private RaycastHit hit;
    
    public UnitData PlaceDownUnit(bool red, int health, int strength, int speed, int defence)
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out hit, 100, layer))
        {
            GameObject SpawnedUnit = Instantiate(Unit, hit.point + (Vector3.up), Quaternion.identity);
            SpawnedUnit.GetComponent<UnitScript>().SetUnitScript(red, health, strength, speed, defence);
            return SpawnedUnit.GetComponent<UnitScript>().GetUnitData();
        }
        return null;
    }
}
