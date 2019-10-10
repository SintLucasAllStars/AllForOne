using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTeam
{
    //Standard Team Variables
    protected int teamNumber;

    //Unit and UnitStore
    protected UnitStore unitStore;

    protected int unitPoints;

    protected List<Unit> teamUnits;
    protected Unit selectedUnit;

    //Ingame
    protected bool yourTurn;


    private void BuyUnit()
    {

    }
}
