using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	private Material _material;

	private int _health;
	private int _speed;
	private int _strength;
	private int _defense;
	public int Player { get; private set; }

	private void Start()
	{
		_material = GetComponentInChildren<SkinnedMeshRenderer>().materials[0];
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
		
		_material.EnableKeyword("_EmissionColor");
		_material.SetColor("_EmissionColor", GameManager.GameState.Players[Player].Color);
	}
}
