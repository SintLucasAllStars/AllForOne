using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitPlacer : Singleton<UnitPlacer>
{
    [SerializeField] Camera sceneCamera;

    public bool unitPlaced;
    public Vector3 unitLocation;
    [SerializeField] LayerMask placeMask;

    void OnClick()
    {
        // Can only place a unit while the gamephase is in UnitPlacing
        if (GameManager.Instance.gamePhase == GameManager.GamePhase.UnitPlacing)
        {
            RaycastHit hit;
            Ray ray = sceneCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out hit, 100, placeMask))
            {
                // Get location of raycast hit and save in unitLocation
                unitLocation = hit.point;
                unitPlaced = true;
            }
        }
    }
}
