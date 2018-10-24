using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

	[SerializeField] private GameObject _unitCreationScreen;

	private void OnEnable()
	{
		GameManager.OnStartSetup += OnStartSetup;
	}

	private void OnDisable()
	{
		GameManager.OnStartSetup -= OnStartSetup;
	}

	private void OnStartSetup()
	{
		Instantiate(_unitCreationScreen, transform);
	}
}
