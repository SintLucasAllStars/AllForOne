using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public int health;
    public int strength;
    public int defence;
    public int speed;

    public Unit(int _health, int _strength, int _defence, int _speed)
    {
        health = _health;
        strength = _strength;
        defence = _defence;
        speed = _speed;
    }

    public void GetHit(int damage)
    {
        health -= damage;
        if(health > 0)
        {
            Debug.Log("Health left is = " + health);
        }
        else
        {
            Debug.Log("Unit has no health left");
        }
    }
}
