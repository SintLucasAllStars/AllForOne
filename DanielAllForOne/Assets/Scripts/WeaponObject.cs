using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponTypes
{
    Punch,
    Knife,
    WarHammer,
    Gun
}

public class WeaponObject : MonoBehaviour
{
    public WeaponInfo WeaponInfo;

    public WeaponObject EquipWeapon()
    {
        gameObject.SetActive(true);
        return this;
    }

    public void UnEquipWeapon()
    {
        if (WeaponInfo.weaponType != WeaponTypes.Punch)
            gameObject.SetActive(false);
    }
}
[System.Serializable]
public struct WeaponInfo
{
    public WeaponTypes weaponType;
    public float Damage;
    public float Speed;
    public float Range;
}
