using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameState GameState;
	public static GameManager Instance;
	
	public delegate void StartSetup();
	public delegate void StartGame();

	public static event StartSetup OnStartSetup ;
	public static event StartGame OnStartGame ;
	private void Awake()
	{
		if(Instance != this && Instance != null) Destroy(this);
		else
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
	}

	public void StartNewGame()
	{
		GameState = new GameState(new Player[2]{
		new Player(100, 1, new Color(1,0.3f,0.3f)),
		new Player(100, 2, new Color(0.3f,0.3f,1f)),
		});

		if (OnStartSetup != null) OnStartSetup();
	}

	private void Start()
	{
		StartNewGame();
	}

	public void EndSetup()
	{
		Debug.Log("EndSetup");
		GameState.CurrentState = State.Playing;
		if (OnStartGame != null) OnStartGame();
	}
}
