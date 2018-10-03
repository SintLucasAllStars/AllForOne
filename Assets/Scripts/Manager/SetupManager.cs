using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
	public GameObject selectCanvas;

	[SerializeField]
	float maxSpeed;
	[SerializeField]
	float maxHealth;
	[SerializeField]
	float maxStrength;
	[SerializeField]
	float maxDefense;

	float minSpeed;
	float minHealth;
	float minStrength;
	float minDefense;

	[SerializeField]
	Slider speed;
	[SerializeField]
	Slider health;
	[SerializeField]
	Slider strength;
	[SerializeField]
	Slider defense;
	[SerializeField]
	Text cost;

	public int costOfNewUnit = 0;

	TurnController turnController;
	bool readyUnit = false;
	// Use this for initialization
	void Start()
	{
		minSpeed = (maxSpeed / 100);
		minHealth = (maxHealth / 100);
		minStrength = (maxStrength / 100);
		minDefense = (maxDefense / 100);
		//speed.minValue = minSpeed;
	//	speed.maxValue = maxSpeed;
		//health.minValue = minHealth;
       // health.maxValue = maxHealth;
		//strength.minValue = minStrength;
		//strength.maxValue = maxStrength;
		//defense.minValue = minDefense;
		//defense.maxValue = maxDefense;
		turnController = GetComponent<TurnController>();
		selectCanvas.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{
		if (selectCanvas.activeSelf)
		{
			costOfNewUnit = 0;
			costOfNewUnit += Mathf.CeilToInt(speed.value * 4);
			costOfNewUnit += Mathf.CeilToInt(health.value  * 4);
			costOfNewUnit += Mathf.CeilToInt(strength.value  * 1);
			costOfNewUnit += Mathf.CeilToInt(defense.value  * 1);
			cost.text = "Cost: "+ costOfNewUnit.ToString();
		}
		if (readyUnit)
		{

		}
	}
	public void DoneCreatingUnit()
	{

		selectCanvas.SetActive(false);
		readyUnit = true;
	}
	void DoneSpawningUnit()
	{
		turnController.NextTeam();
		selectCanvas.SetActive(true);
	}
}
