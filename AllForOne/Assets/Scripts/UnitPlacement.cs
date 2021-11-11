using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    public GameObject unit;

    private void Update()
    {
        if (GameManager.instance.placeUnit)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            { 
                if (hit.collider.CompareTag("House") && Input.GetMouseButtonDown(0))
                {
                    Instantiate(unit, hit.point, Quaternion.identity);

                    GameManager.instance.playerTurn = !GameManager.instance.playerTurn;

                    GameManager.instance.placeUnit = false;
                }
            }
        }
    }
}
