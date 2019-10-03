using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType { Punch, PowerPunch, Knife, Gun, Warhammer}
    public WeaponType weaponType;
    public int damage;
    public int speed;
    public int range;
}
