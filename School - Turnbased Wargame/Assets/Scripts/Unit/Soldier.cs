using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Soldier : Unit
{
    public ushort cost;
    public WeaponAsset weapon;

    public override void Place()
    {
       
    }

    public override void CreateUnit(ushort health, ushort strength, ushort speed, ushort defense, ushort cost)
    {
        base.CreateUnit(health, strength, speed, defense, cost);
        this.cost = cost;
    }
}


