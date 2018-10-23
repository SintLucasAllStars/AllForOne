using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
	public int PlayerNumber { get; private set; }
	public int Points;
	public Color Color;
	public List<Unit> Units = new List<Unit>();

	public Player(int points, int playerNumber, Color color)
	{
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
}

