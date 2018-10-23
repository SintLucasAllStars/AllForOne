using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
	public int PlayerNumber { get; private set; }
	public int Points;
	public Color Color;
	public List<Unit> Units { get; private set; } 
	public List<PowerUp> PowerUps { get; private set; } 

	public Player(int points, int playerNumber, Color color)
	{
		Units = new List<Unit>();
		PowerUps = new List<PowerUp>();
		Points = points;
		PlayerNumber = playerNumber;
		Color = color;
	}

	public void RemoveUnit(Unit unit)
	{
		Units.Remove(unit);
	}

	public void AddUnit(Unit unit)
	{
		Units.Add(unit);
	}

	public void AddPowerUp(PowerUp powerUp)
	{
		PowerUps.Add(powerUp);
	}
	
	public void RemovePowerUp(PowerUp powerUp)
	{
		PowerUps.Remove(powerUp);
	}
}

