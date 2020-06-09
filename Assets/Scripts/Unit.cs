using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    int _health;
    int _strength;
    int _speed;
    int _defense;

    bool _isAlive = true;
    bool _isFortied;
    Weapon _weapon;

    public int Attack()
    {
        return _strength * _weapon.GetDamage();
    }

    public void TakeHit(int a_Damage)
    {
        if(_health - a_Damage > 0)
        {
            _health -= a_Damage;
        }
        else
        {
            _health = 0;
            _isAlive = false;
        }
    }
}