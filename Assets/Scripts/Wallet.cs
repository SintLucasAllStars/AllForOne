using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallet
{
    private int _money;
    public int Money => _money;

    public Wallet(int startingMoney) => _money = startingMoney;

    public Wallet()
    { }

    public bool HasEnoughMoney(int money)
    {
        if (_money - money < 0)
            return false;

        return true;
    }

    public void Deposit(int money)
    {
        if (money < 0)
            return;

        _money += money;
    }

    public void Withdraw(int money)
    {
        if (money < 0)
            return;

        _money -= money;
    }
}
