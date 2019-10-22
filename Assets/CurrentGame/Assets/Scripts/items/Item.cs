using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{

    public abstract class Weapon : MonoBehaviour
    {

        public string Name;
        public int Damage;
        public int Speed;
        public int Range;

        public enum WeaponType
        {
            PowerPunch, Knife, Warhammer, Gun,punch
        }

        public WeaponType Type;
    }
}

