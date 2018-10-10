using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PowerUp-", menuName = "PowerUp", order = 1)]
public class PowerUp : ScriptableObject
{
    public int powerUpID;
	public float percentageStrength;
	public float percentageSpeed;
	public float percentageDefense;
	public float healthBoost;
	public float freezeTime;
}