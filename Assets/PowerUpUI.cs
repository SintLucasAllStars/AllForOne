using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
	private Image _image;
	private Sprite _defaultSprite;

	private bool _isEmpty;

	private List<PowerUp> _powerUps;
	private PowerUp _selected;

	private int _selectedNumber = 0;

	private int _currentPlayer;
	
	void Start ()
	{
		_image = GetComponent<Image>();
		_defaultSprite = _image.sprite;
	}

	private void OnEnable()
	{
		RoundManager.OnStartRound += LoadPowerUps;
		RoundManager.OnEndRound += ClearPowerUps;
	}

	private void OnDisable()
	{
		RoundManager.OnStartRound -= LoadPowerUps;
		RoundManager.OnEndRound -= ClearPowerUps;
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			_selectedNumber = (_selectedNumber + 1) % _powerUps.Count;
			SetCurrent();
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			_selectedNumber = (_selectedNumber - 1) % _powerUps.Count;
			SetCurrent();
		}
		else if (Input.GetKeyDown(KeyCode.Q))
		{
			_selected.Activate(RoundManager.Instance.CurrentUnit);
			GameManager.GameState.Players[_currentPlayer].RemovePowerUp(_selected);
			ReloadPowerUps();
		}
	}

	public void ReloadPowerUps()
	{
		LoadPowerUps(_currentPlayer);
	}

	public void LoadPowerUps(int player)
	{
		_currentPlayer = player;
		
		_selected = null;
		_selectedNumber = 0;
		
		_powerUps = GameManager.GameState.Players[player].PowerUps;

		if (_powerUps.Count == 0)
		{
			
			_image.sprite = _defaultSprite;
			_isEmpty = true;
			return;
		}
		
		SetCurrent();
	}

	public void ClearPowerUps()
	{
		_selectedNumber = 0;
		_selected = null;
		_image.sprite = _defaultSprite;
	}

	private void SetCurrent()
	{
		_selected = _powerUps[_selectedNumber];
		_image.sprite = _selected.Image;
	}
}
