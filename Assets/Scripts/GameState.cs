using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
	public Player[] Players;
	public int Round;
	public State CurrentState;

	public GameState(Player[] players)
	{
		Players = players;
		CurrentState = State.Startup;
	}
}

public enum State
{
	Startup,
	Playing,
	Finished,
}
