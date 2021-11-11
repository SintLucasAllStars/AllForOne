using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacement : MonoBehaviour
{
    public LayerMask layerMask;
    Ray ray;
    RaycastHit hitData;
    Vector3 worldPosition;
    public Camera overviewCamera;
    
    private Unit currentUnit;
    public GameObject unitPrefab;
    public UnitCreator unitCreator;
    
    void Update()
    {
        // Prevents spawning units unless unit values are passed from UnitCreator.
        if (Input.GetMouseButtonDown(0) && currentUnit != null)
        {
            ray = overviewCamera.ScreenPointToRay(Input.mousePosition);
            
            // Only allow placing units in designated areas using the UnitPlacement layer mask.
            if (Physics.Raycast(ray, out hitData, 50, layerMask))
            {
                worldPosition = hitData.point;
                GameObject unit = Instantiate(unitPrefab,worldPosition, Quaternion.identity);
                unit.GetComponent<UnitBehaviour>().AddStats(currentUnit);
                unitCreator.GetPlayer().AddUnit(unit);
                
                currentUnit = null;
                // Flip to the other player and update the UI accordingly.
                unitCreator.FlipPlayer();
                unitCreator.ActivateGUI();
                unitCreator.UpdatePlayerDataUI();
            }
        }
    }
    
    public void SetCurrentUnit(Unit unit)
    {
        currentUnit = unit;
    }
}