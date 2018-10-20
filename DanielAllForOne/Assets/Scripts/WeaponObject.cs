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


public class WeaponObject : MonoBehaviour {

    public WeaponTypes weaponType;
    public Stats WeaponStats;

    private void OnTriggerEnter(Collider other)
    {
        
    }

    [System.Serializable]
    public struct Stats
    {
        public float Damage;
        public float Speed;
        public float Range;
    }
}
