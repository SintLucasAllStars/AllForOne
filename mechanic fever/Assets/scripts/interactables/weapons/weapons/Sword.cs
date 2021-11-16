using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override void WeaponSetup()
    {
        index = 2;
        damage = 3;
        speed = 8;
        range = 0;
    }
}
