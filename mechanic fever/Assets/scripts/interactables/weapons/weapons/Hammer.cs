using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Weapon
{
    public override void WeaponSetup()
    {
        index = 3;
        damage = 8;
        speed = 4;
        range = 1;
    }
}
