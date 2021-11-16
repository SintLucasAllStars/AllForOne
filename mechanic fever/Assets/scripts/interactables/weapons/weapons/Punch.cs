using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Weapon
{
    public override void WeaponSetup()
    {
        index = 0;
        damage = 1;
        speed = 10;
        range = 0;
    }
}
