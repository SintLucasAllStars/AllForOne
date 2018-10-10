using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	private int _health;
	private int _speed;
	private int _strength;
	private int _defense;
	private bool _isRed;

	public void SetValues(bool isRed, int health, int speed, int strength, int defense)
	{
		_isRed = isRed;
		_health = health;
		_speed = speed;
		_strength = strength;
		_defense = defense;
	}
}
