using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeMachine : PowerUp
{
	public TimeMachine(Sprite newIcon)
	{
		icon = newIcon;
	}

	public override void Use(Character plyrChar)
	{
		plyrChar.M_Pc.timerPaused = true;
		plyrChar.StartCoroutine(plyrChar.ResetStats(PowerUpType.TimeMachine, 3));
	}
}