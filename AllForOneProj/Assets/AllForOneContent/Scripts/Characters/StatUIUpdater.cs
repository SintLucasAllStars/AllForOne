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
		health.text = "Health: " + character.health.ToString();
		strength.text = "Strength: " + character.strength.ToString();
		speed.text = "Speed: " + character.speed.ToString();
		defense.text = "Defense: " + character.defense.ToString();
		
	}
}
