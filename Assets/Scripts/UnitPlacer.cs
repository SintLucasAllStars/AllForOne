using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacer : Singleton<UnitPlacer>
{
    // Update is called once per frame
    void Update()
    {
        // Can only place a unit while the gamephase is in UnitPlacing
        while (GameManager.Instance.gamePhase == GameManager.GamePhase.UnitPlacing)
        {

        }
    }
}
