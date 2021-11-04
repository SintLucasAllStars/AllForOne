using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Player
{
    private int currency;

    public Player(int maxMoney)
    {
        currency = maxMoney;
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
