using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpType
{
	Rage,
	TimeMachine,
	Adrenaline
}

public class PowerUpPickUp : interactable
{
	public PowerUpType m_Type;
	public Sprite m_ItemIcon;

	public override void Pickup(Character character)
	{
		switch (m_Type)
		{
			case PowerUpType.Rage:
				Rage Rage = new Rage(m_ItemIcon);
				character.AddPowerUp(Rage, 1);
				break;
			case PowerUpType.TimeMachine:
				TimeMachine TimeMachine = new TimeMachine(m_ItemIcon);
				character.AddPowerUp(TimeMachine, 0);
				
				break;
			case PowerUpType.Adrenaline:
				Adrenaline Adrenaline = new Adrenaline(m_ItemIcon);
				character.AddPowerUp(Adrenaline, 2);
				break;
			default:
				break;
		}
		Destroy(this.gameObject);
		return;
	}

}
