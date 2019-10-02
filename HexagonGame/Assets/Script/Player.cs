using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private int money;
    private Warrior[] warriors;

    public Player()
    {
        this.name = "Unnamed";
        this.money = 100;
    }

    public Player(string a_Name, int a_Money)
    {
        this.name = a_Name;
        this.money = a_Money;
    }
}
