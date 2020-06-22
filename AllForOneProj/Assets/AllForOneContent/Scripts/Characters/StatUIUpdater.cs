using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIUpdater : MonoBehaviour
{
	public TMP_Text health;
	public TMP_Text strength;
	public TMP_Text speed;
	public TMP_Text defense;
	public Character character;

    public void UpdateText()
	{
		health.text = "Health: " + character.m_PlayerStats.m_Health.ToString();
		strength.text = "Strength: " + character.m_PlayerStats.m_Strength.ToString();
		speed.text = "Speed: " + character.m_PlayerStats.m_Speed.ToString();
		defense.text = "Defense: " + character.m_PlayerStats.m_Defense.ToString();
		
	}
}
