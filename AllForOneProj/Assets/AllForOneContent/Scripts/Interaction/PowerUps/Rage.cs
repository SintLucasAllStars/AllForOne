using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rage : PowerUp
{
	
	public Rage(Sprite newIcon)
	{
		icon = newIcon;
	}

	public override void Use(Character plyrChar)
	{
		plyrChar.m_PlayerStats.m_Strength += (plyrChar.m_PlayerStats.m_Strength * 0.1f);
		plyrChar.StartCoroutine(plyrChar.ResetStats(PowerUpType.Rage, 5));
	}
}
