using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Adrenaline : PowerUp
{
	public Adrenaline(Sprite newIcon)
	{
		icon = newIcon;
	}

	public override void Use(Character plyrChar)
	{
		plyrChar.m_PlayerStats.m_Speed += (plyrChar.m_PlayerStats.m_Speed / 2);
		plyrChar.StartCoroutine(plyrChar.ResetStats(PowerUpType.Adrenaline, 10));
		Debug.Log("Speedy");
	}
}
