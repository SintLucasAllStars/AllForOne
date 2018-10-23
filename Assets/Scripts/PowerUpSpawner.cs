using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
	[SerializeField]
	private Transform[] _locations;
	[SerializeField]
	private PowerUp[] _powerUps;

	[SerializeField] 
	private GameObject _powerUpObject;

	private List<PowerUpPoint> _currentPowerUps = new List<PowerUpPoint>();

	private void OnEnable()
	{
		RoundManager.OnStartRound += OnStartRound;
	}

	private void OnDisable()
	{
		RoundManager.OnStartRound -= OnStartRound;
	}

	private void OnStartRound(int player)
	{
		int rand = Random.Range(0, _powerUps.Length+1);
		Debug.Log(rand);
		foreach (PowerUpPoint powerUp in _currentPowerUps)
		{
			if (powerUp.transform.position == _locations[rand].position)
			{
				OnStartRound(player);
				return;
			}
		}
		
		

		GameObject newObject = Instantiate(_powerUpObject, _locations[rand].position, Quaternion.identity);
		PowerUpPoint newPowerUp = newObject.GetComponent<PowerUpPoint>();
		newPowerUp.PowerUp = _powerUps[Random.Range(0, _powerUps.Length)];
		_currentPowerUps.Add(newPowerUp);
	}

	public void RemovePoint(PowerUpPoint point)
	{
		_currentPowerUps.Remove(point);
	}
}
