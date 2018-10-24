using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour {
    public Unit currentUnit;
    [HideInInspector]
    public TeamManager tm;
    public Camera c;
    public void Update()
    {
        tm = FindObjectOfType<TeamManager>();
        SelectUnit();
    }
    void SelectUnit()
    {
        RaycastHit hit;
        Ray ray = c.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform hitTransform = hit.transform;
            if (hit.transform.GetComponent<Unit>() && hit.transform.GetComponent<Unit>().team == tm.CurrentTeam)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Unit u = hit.transform.GetComponent<Unit>();
                    currentUnit = u;
                    u.OnSelected();
                }
            }

        }
    }
}
