using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapon-", menuName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
	public int weaponID;
	public float damage = 5;
    public float cooldown = 1;
    public float range = 1;
}