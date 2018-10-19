using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Punch,
    PowerPunch,
    Knife,
    Warhammer,
    Gun
}

public class Weapon
{
    public int Damage;
    public int Speed;
    public int Range;

    public Weapon(int damage, int speed, int range)
    {
        Damage = damage;
        Speed = speed;
        Range = range;
    }
}

public static class WeaponManager
{
    public static Dictionary<WeaponTypes, Weapon> Weapons = new Dictionary<WeaponTypes, Weapon>
    {
        { WeaponTypes.Punch, new Weapon(1,10,0) }
    };
}
