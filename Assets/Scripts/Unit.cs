using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	private Material _material;
	private SpriteRenderer _marker;

	private int _health;
	private int _speed;
	private int _strength;
	private int _defense;
	public int Player { get; private set; }

	private void Start()
	{
		_material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
		_marker = GetComponentInChildren<SpriteRenderer>();
	}

	public void SetValues(int player, int health, int speed, int strength, int defense)
	{
		Player = player;
		_health = health;
		_speed = speed;
		_strength = strength;
		_defense = defense;
	
		SetColor();
	}

	private void SetColor()
	{
		if(_material == null) _material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
		if (_marker == null) _marker = GetComponentInChildren<SpriteRenderer>();
		
		_material.EnableKeyword("_EmissionColor");
		_material.SetColor("_EmissionColor", GameManager.GameState.Players[Player].Color);

		_marker.color = GameManager.GameState.Players[Player].Color;
	}

	public void ChangeStat(PlayerStats stat, int amount)
	{
		switch (stat)
		{
			case PlayerStats.Health:
				_health += amount;
				if (_health <= 0) Death();
				break;
			case PlayerStats.Speed:
				_speed += amount;
				break;
			case PlayerStats.Strength:
				_strength += amount;
				break;
			case PlayerStats.Defense:
				_defense += amount;
				break;
			default:
				throw new ArgumentOutOfRangeException("stat", stat, null);
		}
	}

	public int GetStat(PlayerStats stat)
	{
		switch (stat)
		{
			case PlayerStats.Health:
				return _health;
			case PlayerStats.Speed:
				return _speed;
			case PlayerStats.Strength:
				return _strength;
			case PlayerStats.Defense:
				return _defense;
			default:
				throw new ArgumentOutOfRangeException("stat", stat, null);
		}
	}

	private void Death()
	{
		GameManager.GameState.Players[Player].RemoveUnit(this);
		Destroy(gameObject);
	}
}

public enum PlayerStats
{
	Health,
	Speed,
	Strength,
	Defense,
}
