using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    public List<GameObject> units;
    private int currency;

    public Player(int maxMoney)
    {
        currency = maxMoney;
        units = new List<GameObject>();
    }

    public void setUnits(GameObject[] givenUnits)
    {
        foreach(GameObject unit in givenUnits)
        {
            units.Add(unit);
        }
    }

    public GameObject getUnit(int index)
    {
        return units[index];
    }

    public void RemoveUnit(GameObject unit)
    {
        units.Remove(unit);
    }

    //public void UpdateUnitsList()
    //{
    //    units.RemoveAll(n => n.CompareTag("Untagged"));
    //}

    public List<GameObject> GetUnitList()
    {
        return units;
    }

    public int getUnitLenght()
    {
        return units.Count;
    }

    #region currency management

    public int getCurrency()
    {
        return currency;
    }

    public void zeroCurrency()
    {
        currency = 0;
    }

    public bool BuyCharacter(int cost)
    {
        bool value = false;

        if (currency >= cost)
        {
            value = true;
            currency -= cost;
        }
        
        return value;
    }
    #endregion
}
