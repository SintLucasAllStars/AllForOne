using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUps/TimePowerUp", order = 1)]
public class TimePowerUp : PowerUp
{

	public int Duration;
	
	public override void Activate(Unit unit)
	{
		RoundManager.Instance.IsPaused = true;
		GameManager.Instance.StartCoroutine(DeactivatePowerUp());
	}

	IEnumerator DeactivatePowerUp()
	{
		yield return new WaitForSeconds(Duration);
		RoundManager.Instance.IsPaused = false;
	}
}
