using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreation : MonoBehaviour
{
	[SerializeField] private GameObject _unitPrefab;

	[SerializeField] private Slider _healthSlider;
	[SerializeField] private Slider _strengthSlider;
	[SerializeField] private Slider _speedSlider;
	[SerializeField] private Slider _defenseSlider;
	[SerializeField] private Text _costText;

	private int cost;

	private void Start()
	{
		RandomizeValues();
		UpdateValues();
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
		cost = (3 * (int)_healthSlider.value) + (3*(int)_speedSlider.value) + (2*(int)_strengthSlider.value) + (2*(int)_defenseSlider.value);
		_costText.text = cost.ToString();
	}

	public void HireUnit()
	{
		GameObject newUnit = _unitPrefab;
		newUnit.GetComponent<Unit>().SetValues(true, (int)_healthSlider.value, (int)_speedSlider.value, (int)_strengthSlider.value, (int)_defenseSlider.value);
		UnitPlacer.Instance.PlaceObject(newUnit);
		gameObject.SetActive(false);
	}
}
