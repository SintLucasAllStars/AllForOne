using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "AllForOne/WeaponStats", order = 0)]
public class WeaponStats : ScriptableObject
{
    public AnimationClip Animation { get { return animation; } }
    public float Damage { get { return damage; } }
    public float Speed { get { return speed; } }
    public float Range { get { return range; } }

    [SerializeField] private AnimationClip animation;
    [SerializeField] private float damage = 0;
    [SerializeField] private float speed = 0;
    [SerializeField] private float range = 0;
}
