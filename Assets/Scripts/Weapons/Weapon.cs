using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon
{
    public float Damage;
    public float Speed;
    public float Range;

    public enum WeaponEnum
    {
        Fists,
        Gloves,
        Knife,
        WarHammer,
        Gun,
    }

    public WeaponEnum MyCurrentWeapon;


    public bool RestrictAnimationRotation()
    {
        return MyCurrentWeapon == WeaponEnum.Gun || MyCurrentWeapon == WeaponEnum.WarHammer;
    }
    

    public Weapon(float damage, float speed, float range, WeaponEnum weaponEnum)
    {
        Range = range;
        Speed = speed;
        Damage = damage;
        MyCurrentWeapon = weaponEnum;
    }


}
