using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{

    public abstract class Character : MonoBehaviour
    {

        public string Name;
        public float Strength;
        public float Defense;
        public float Health;
        public float MoveSpeed;
        public float AttackSpeed;
        public enum AttType
        {
            Melee, PhysicalRange, MagicalRange
        }

        public AttType AttackType;
        public enum Type
        {
            Warrior, Barbarian, Knight, Pikeman, Heavy, BladeMaster, Assassin, Archer, Gunner, Bomber, Sniper, Mage, PlagueBearer, Enchanter, Witch
        }

        public float jumpPower;
        public int WhichSide;

    }
}

