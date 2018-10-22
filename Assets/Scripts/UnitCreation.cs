using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreation : MonoBehaviour
{
	public static UnitCreation Instance;
	
	[SerializeField] private GameObject _unitPrefab;

	[SerializeField] private Slider _healthSlider;
	[SerializeField] private Slider _strengthSlider;
	[SerializeField] private Slider _speedSlider;
	[SerializeField] private Slider _defenseSlider;
	[SerializeField] private Text _costText;
	[SerializeField] private Image _background;
	[SerializeField] private Text _title;
	[SerializeField] private SkinnedMeshRenderer _skinnedMesh;
	private Material _previewMaterial;

	private RectTransform _rectTransform;

	private int _currentPlayer = 0;
	private GameState _currentState;

	private int _cost;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_currentState = GameManager.GameState;
		_rectTransform = GetComponent<RectTransform>();
		_previewMaterial = _skinnedMesh.materials[0];
		LoadScreen();
	}

	public void LoadScreen()
	{
		if(_currentState == null) _currentState = GameManager.GameState;
		
		_currentPlayer = (_currentPlayer + 1) % _currentState.Players.Length;

		if (_currentState.Players[_currentPlayer].Points < 10)
		{
			if (_currentState.Players[(_currentPlayer + 1) % _currentState.Players.Length].Points < 10)
			{
				GameManager.Instance.EndSetup();
				CameraController.Instance.SetCameraState(CameraState.TopDown);
				Destroy(gameObject);
				return;
			}
			else
			{
				LoadScreen();
				return;
			}
		}
		
		SetPreviewColor();
		
		_background.color = _currentState.Players[_currentPlayer].Color;
		_title.text = "Player " + (_currentPlayer + 1) + "  -  " + _currentState.Players[_currentPlayer].Points;
		RandomizeValues();
		UpdateValues();
		_rectTransform.anchoredPosition = Vector2.zero;
	}

	private void SetPreviewColor()
	{
		_previewMaterial.EnableKeyword("_EmissionColor");
		_previewMaterial.SetColor("_EmissionColor", _currentState.Players[_currentPlayer].Color);
	}

	public void RandomizeValues()
	{
		_healthSlider.value = Random.Range(4, 7);
		_strengthSlider.value = Random.Range(4, 7);
		_speedSlider.value = Random.Range(4, 7);
		_defenseSlider.value = Random.Range(4, 7);
	}

	public void UpdateValues()
	{
		_cost = (3 * (int)_healthSlider.value) + (3*(int)_speedSlider.value) + (2*(int)_strengthSlider.value) + (2*(int)_defenseSlider.value);
		_costText.text = _cost.ToString();
	}

	public void HireUnit()
	{
		if(_currentState.Players[_currentPlayer].Points < _cost) return;
		CameraController.Instance.SetCameraState(CameraState.TopDown);
		_currentState.Players[_currentPlayer].Points -= _cost;
		GameObject newUnit = Instantiate(_unitPrefab, Vector3.one*100,Quaternion.identity);
		newUnit.GetComponent<Unit>().SetValues(_currentPlayer, (int)_healthSlider.value, (int)_speedSlider.value, (int)_strengthSlider.value, (int)_defenseSlider.value);
		UnitPlacer.Instance.PlaceObject(newUnit);
		_currentState.Players[_currentPlayer].Units.Add(newUnit.GetComponent<Unit>());
		_rectTransform.anchoredPosition = Vector2.up * 1000;
	}
}
