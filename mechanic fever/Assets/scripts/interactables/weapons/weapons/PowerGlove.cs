using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerGlove : Weapon
{
    public override void WeaponSetup()
    {
        index = 1;
        damage = 2;
        speed = 10;
        range = 0;
    }
}
