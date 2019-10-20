using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private int money;
    private List<Actor> warriors;

    public Player()
    {
        this.name = "Unnamed";
        InitPlayer();
    }

    public Player(string a_Name)
    {
        this.name = a_Name;
        InitPlayer();
    }

    public string GetName() { return this.name; }

    public int GetMoney() { return this.money; }
    public void SetMoney(int a_Money) { this.money = a_Money; }

    public void AddWarrior(Actor a_Warrior)
    {
        warriors.Add(a_Warrior);
    }

    private void InitPlayer()
    {
        warriors = new List<Actor>();
        this.money = 100;
    }
}
