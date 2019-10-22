using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private int money;
    private Color color;
    private List<Actor> warriors;

    public Player()
    {
        this.name = "Unnamed";
        InitPlayer();
    }

    public Player(string a_Name, Color a_Color)
    {
        this.name = a_Name;
        this.color = a_Color;
        InitPlayer();
    }

    public void AddWarrior(Actor a_Warrior)
    {
        warriors.Add(a_Warrior);
    }

    public void RemoveWarrior(Actor a_Warrior)
    {
        warriors.Remove(a_Warrior);
    }

    public bool CompareWarrior(Actor a_Warrior)
    {
        if (warriors.Contains(a_Warrior))
        {
            return true;
        }
        return false;
    }

    public void RemoveWarriorCost(int a_Cost)
    {
        money -= a_Cost;
    }

    private void InitPlayer()
    {
        warriors = new List<Actor>();
        this.money = 100;
    }

    public string GetName() { return this.name; }

    public List<Actor> GetWarriors()
    {
        return warriors;
    }

    public int GetMoney() { return this.money; }
    public void SetMoney(int a_Money) { this.money = a_Money; }
    public Color GetColor(){ return color; }
}
