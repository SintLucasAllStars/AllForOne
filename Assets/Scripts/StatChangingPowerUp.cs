using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PowerUp", menuName = "PowerUps/StatPowerUp", order = 1)]
public class StatChangingPowerUp : PowerUp
{

	public PlayerStats StatType;
	public int Amount;
	public int Duration;

	private Unit _currentUnit;

	public override void Activate(Unit unit)
	{
		unit.ChangeStat(StatType, Amount);
		_currentUnit = unit;
		GameManager.Instance.StartCoroutine(ClearEffect());
	}

	IEnumerator ClearEffect()
	{
		yield return new WaitForSeconds(Duration);
		_currentUnit.ChangeStat(StatType, -Amount);
	}
}
