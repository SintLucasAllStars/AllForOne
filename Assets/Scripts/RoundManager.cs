using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
	public static RoundManager Instance;

	private UnitSelector _unitSelector;
	
	private int _currentPlayer = 0;
	public Unit CurrentUnit { get; private set; }

	[SerializeField]
	private Image _counterImage;

	public bool IsPaused;
	
	public delegate void StartRound(int player);
	public delegate void EndRound();

	public static event EndRound OnEndRound;
	public static event StartRound OnStartRound;

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
		GameManager.OnStartGame += StartNewRound;
	}

	private void OnDisable()
	{
		GameManager.OnStartGame -= StartNewRound;
	}



	private void StartNewRound()
	{
		_unitSelector.SelectUnit(_currentPlayer);
		_counterImage.color = GameManager.GameState.Players[_currentPlayer].Color;
	}

	public void SelectedUnit(Unit unit)
	{
		StartCoroutine(Round(10));
		if (OnStartRound != null) OnStartRound(_currentPlayer);
		CurrentUnit = unit;
	}

	public void EndCurrentRound()
	{
		if (OnEndRound != null) OnEndRound();
		_counterImage.fillAmount = 1;
		CurrentUnit = null;
		_unitSelector.ClearCurrentUnit();
		_currentPlayer = (_currentPlayer + 1) % GameManager.GameState.Players.Length;
		StartNewRound();
	}

	IEnumerator Round(int roundLength)
	{
		while (CameraController.Instance.IsMoving)
		{
			yield return new WaitForEndOfFrame();
		}
		
		int count = roundLength;
		while (count > 0)
		{
			while (IsPaused)
			{
				yield return new WaitForSeconds(0.1f);
			}
			count--;
			_counterImage.fillAmount = (float)count / (float)roundLength;
			yield return new WaitForSeconds(1f);
		}
		
		EndCurrentRound();
	}
}
