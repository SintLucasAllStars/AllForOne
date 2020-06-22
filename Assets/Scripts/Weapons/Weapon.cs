using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName;

    int _damage = 1;
    int _speed = 10;
    int _range = 0;

    public int GetDamage()
    {
        return _damage;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public int GetRange()
    {
        return _range;
    }
}
