using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMono : MonoBehaviour
{
    public Weapon MyWeapon;
    public float Damage;
    public float Range;
    public float Speed;

    private void Start()
    {
        MyWeapon = new Weapon(Damage,Speed,Range);
    }

}
