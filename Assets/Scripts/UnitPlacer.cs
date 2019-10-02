using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacer : MonoBehaviour
{

    public GameObject unitP1, unitP2;
    public GameObject UnitListP1, UnitListP2;

    public bool switchTurn;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
 {
            switchTurn = !switchTurn;
        }

    }

    void OnMouseDown()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            if(switchTurn == false)
            {
                GameObject newUnit = Instantiate(unitP1, hit.point, Quaternion.identity);
                newUnit.transform.parent = UnitListP1.transform;
            }
            else
            {
                GameObject newUnit = Instantiate(unitP2, hit.point, Quaternion.identity);
                newUnit.transform.parent = UnitListP2.transform;
            }
        }
    }
}
