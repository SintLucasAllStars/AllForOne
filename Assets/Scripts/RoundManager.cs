using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
	public static RoundManager Instance;

	private UnitSelector _unitSelector;
	
	private int _currentPlayer;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_unitSelector = FindObjectOfType<UnitSelector>();
	}

	private void OnEnable()
	{
		GameManager.OnStartRound += OnStartRound;
	}
	
	private void OnDisable()
	{
		GameManager.OnStartRound -= OnStartRound;
	}

	private void OnStartRound(int player)
	{
		_currentPlayer = player;
	}
}
