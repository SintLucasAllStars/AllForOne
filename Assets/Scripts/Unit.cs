using UnityEngine;

[System.Serializable]
public class Unit : MonoBehaviour
{
    public int health, strength, speed, defense;

    public int _health
    {
        get { return this.health; }
        set { this.health = _health; }
    }
    public int _strength
    {
        get { return this.strength; }
        set { this.strength = _strength; }
    }
    public int _speed
    {
        get { return this.speed; }
        set { this.speed = _speed; }
    }
    public int _defense
    {
        get { return this.defense; }
        set { this.defense = _defense; }
    }

    //Constructor
    public Unit(int health, int strength, int speed, int defense)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
    }

    public void SpawnWithValues(int health, int strength, int speed, int defense)
    {
        this.health = health;
        this.strength = strength;
        this.speed = speed;
        this.defense = defense;
    }
}
