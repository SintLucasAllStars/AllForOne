using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTeam
{
    //Standard Team Variables
    protected int teamNumber;

    //Unit and UnitStore
    [SerializeField] private UnitStore unitStore;
    protected int unitPoints;

    protected List<Unit> teamUnits;
    protected Unit selectedUnit;

    //Ingame
    protected bool yourTurn;

    /// <summary>
    /// Create a unit and Instantiate it on the map using the UnitStore.
    /// </summary>
    public void BuyUnit()
    {

    }

    public void SelectUnit()
    {

    }
}