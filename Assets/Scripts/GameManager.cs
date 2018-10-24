using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameState GameState;
	public static GameManager Instance;
	
	public delegate void StartSetup();
	public delegate void StartGame();
	public delegate void EndGame();

	public static event StartSetup OnStartSetup ;
	public static event StartGame OnStartGame ;
	public static event EndGame OnEndGame;
	
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

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode arg1)
	{
		if (scene.name == "Game")
		{
			StartNewGame();
		}
	}

	public void EndSetup()
	{
		GameState.CurrentState = State.Playing;
		if (OnStartGame != null) OnStartGame();
	}

	public void StopGame(int losingPlayer)
	{
		RoundManager.Instance.EndCurrentRound();
		RoundManager.Instance.IsPaused = true;
		GameState.CurrentState = State.Finished;
		CameraController.Instance.SetCameraState(CameraState.Static);
		Debug.Log(losingPlayer);
		GameState.Winner = (losingPlayer + 1) % GameState.Players.Length -1;
		Debug.Log(GameState.Winner);
		if (OnEndGame != null) OnEndGame();
	}

	public void ClearState()
	{
		GameState = null;
	}
}
