using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior
{
    private float health;
    private int strength;
    private float speed;
    private int defense;

    private float MaxHealth;

    public GameObject hand;

    private bool isSelected;
    
    public Warrior()
    {
        this.health = 5;
        this.strength = 5;
        this.speed = 0.5f;
        this.defense = 5;
        this.isSelected = false;
        this.MaxHealth = 5;
    }

    public Warrior(int a_Health, int a_Strenth, int a_Speed, int a_Defense)
    {
        this.health = a_Health;
        this.strength = a_Strenth;
        this.speed = (a_Speed);
        this.defense = a_Defense;
        this.isSelected = false;
        this.MaxHealth = a_Health;
    }

    public float GetHealth() { return this.health; }
    public void SetHealth(float a_Health) { this.health = a_Health; }

    public int GetStrenth() { return this.strength; }
    public void SetStrength(int a_Strength) { this.strength = a_Strength; }

    public float GetSpeed() { return this.speed; }
    public void SetSpeed(int a_Speed) { this.speed = a_Speed; }

    public int GetDefense() { return this.defense; }
    public void SetDefense(int a_Defence) { this.defense = a_Defence; }

    public bool GetIsSelected() { return this.isSelected; }
    public void SetIsSelected(bool a_IsSelected) { this.isSelected = a_IsSelected; }

    public float GetMaxHealth() { return this.MaxHealth; }
}
