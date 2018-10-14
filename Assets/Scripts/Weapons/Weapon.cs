using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public float Damage;
    public float Speed;
    public float Range;

    public Weapon(float damage, float speed, float range)
    {
        Range = range;
        Speed = speed;
        Damage = damage;
    }


}
