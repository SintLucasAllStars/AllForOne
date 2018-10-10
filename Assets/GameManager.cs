using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameState GameState;
	
	public delegate void StartSetup();
	public delegate void StartGame();

	public static event StartSetup OnStartSetup ;
	public static event StartGame OnStartGame ;

	public void StartNewGame()
	{
		GameState = new GameState(new Player[2]{
		new Player(100, 1, Color.red),
		new Player(100, 2, Color.blue)
		});
		
		OnStartSetup();
	}
}
