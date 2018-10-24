using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	private SpriteRenderer _fill;
	private Unit _unit;

	void Start ()
	{
		_unit = GetComponentInParent<Unit>();
		_fill = GetComponentsInChildren<SpriteRenderer>()[1];
		_fill.color = GameManager.GameState.Players[_unit.Player].Color;
		_fill.transform.localScale = new Vector3((_unit.GetStat(PlayerStats.Health) / 10f),1,1);
	}

	public void UpdateHealthBar()
	{
		Debug.Log("Fired");
		_fill.transform.localScale = new Vector3((_unit.GetStat(PlayerStats.Health) / 10f),1,1);
	}
}
