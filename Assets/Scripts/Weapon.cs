using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    public float damage;
    public float speed;
    public float range;
}
