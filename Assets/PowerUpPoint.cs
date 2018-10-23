using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPoint : MonoBehaviour
{

	public PowerUp PowerUp;
	private PowerUpUI _powerUpUi;

	private SpriteRenderer _spriteRenderer;

	private PowerUpSpawner _spawner;
	
	void Start ()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_spriteRenderer.sprite = PowerUp.Image;

		_powerUpUi = FindObjectOfType<PowerUpUI>();

		_spawner = FindObjectOfType<PowerUpSpawner>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Unit"))
		{
			GameManager.GameState.Players[other.GetComponent<Unit>().Player].AddPowerUp(PowerUp);
			_powerUpUi.ReloadPowerUps();
			_spawner.RemovePoint(this);
			Destroy(gameObject);
		}
	}
}
