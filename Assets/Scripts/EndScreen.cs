using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
	[SerializeField] private Image _background;
	[SerializeField] private Text _winner;

	private void OnEnable()
	{
		GameManager.OnEndGame += OnEndGame;
	}
	
	private void OnDisable()
	{
		GameManager.OnEndGame -= OnEndGame;
	}


	private void OnEndGame()
	{
		transform.GetChild(0).gameObject.SetActive(true);
		GameState state = GameManager.GameState;
		_background.color = state.Players[state.Winner].Color;
		_winner.text = "Player " + state.Winner;
	}

	public void ReturnToMenu()
	{
		GameManager.Instance.ClearState();
		SceneManager.LoadScene("MainMenu");
	}
}
