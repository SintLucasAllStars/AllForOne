using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private float damage;
    private float speed;
    private float range;
    private string name;
    private GameObject obj;
    private WeaponType weaponType;

    public Item(float a_Damage, float a_Speed, float a_Range, string a_Name, GameObject a_Obj, WeaponType a_WeaponType)
    {
        this.damage = a_Damage;
        this.speed = a_Speed;
        this.range = a_Range;
        this.name = a_Name;
        this.obj = a_Obj;
        obj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        this.weaponType = a_WeaponType;
    }

    public float GetDamage() { return damage; }
    public float GetSpeed() { return speed; }
    public float GetRange() { return range; }
    public WeaponType GetWeaponType() { return weaponType; }
    public string GetName() { return name; }
}

public enum WeaponType
{
    Hand,
    Sword,
    Gun
}
