using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponInformation : MonoBehaviour
{
    public Weapon MyWeapon;
    public float Damage;
    public float Range;
    public float Speed;
    public GameObject Left;
    public GameObject Right;
    public Weapon.WeaponEnum CurrentWeapon;

    protected virtual void Start()
    {
        MyWeapon = new Weapon(Damage,Speed,Range, CurrentWeapon);
    }
    
}
