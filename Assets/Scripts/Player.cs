using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
	public int PlayerNumber { get; private set; }
	public int Points;
	public Color Color;
	public List<Unit> Units;

	public Player(int points, int playerNumber, Color color)
	{
		Points = points;
		PlayerNumber = playerNumber;
		Color = color;
	}
}

