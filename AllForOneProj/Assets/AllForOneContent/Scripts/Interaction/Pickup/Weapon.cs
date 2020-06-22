using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : interactable
{
	[Header("Base")]
	[SerializeField] protected float m_damage;
	[SerializeField] protected float m_Speed;
	[SerializeField] protected float m_Range;

	public override void Use(Character character)
	{
	}
}
