using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupManager : MonoBehaviour
{
	public GameObject selectCanvas;


    //max values so we can easily change it so everything so we can balance it.
	[SerializeField]
	float maxSpeed;
	[SerializeField]
	float maxHealth;
	[SerializeField]
	float maxStrength;
	[SerializeField]
	float maxDefense;

	[Tooltip("layer The Unit can spawn on")]
    public LayerMask layersToRayWith;

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
	[SerializeField]
    Text points;
	[SerializeField]
	Image background;

	int costOfNewUnit = 0;
        
	GameController controller;
	TurnController turnController;
	PlayerPicker playerPicker;
	bool readyUnit = false;
	// Use this for initialization
	void Start()
	{
		controller = GameController.Instance;
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
		playerPicker = GetComponent<PlayerPicker>();
		turnController = GetComponent<TurnController>();
		background.color = controller.teams[controller.currentTeam].teamColor;
		selectCanvas.SetActive(true);
		points.text = "Points left : " + controller.teams[controller.currentTeam].points;
	}

	// Update is called once per frame
	void Update()
	{
		
		if (readyUnit)
		{
			if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                //Ray ray = .ScreenPointToRay(Input.mousePosition);
				Ray ray = controller.topDownViewCamera.ScreenPointToRay(Input.mousePosition);
                //Debug.DrawRay(startRayPoint, -transform.right, Color.green, rayDistance, false);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToRayWith))
                {
					  Unit unit = new Unit();
					unit.unit = SpawnPlayer(hit.point + Vector3.up, controller.teams[controller.currentTeam].teamPrefab).GetComponent<CharacterController>();
					  controller.teams[controller.currentTeam].teamUnits.Add(unit);
					DoneSpawningUnit();
                }
            }
		}
	}
	GameObject SpawnPlayer(Vector3 position, GameObject prefab)
    {
		GameObject game =Instantiate(prefab, position, Quaternion.identity);
		Material material = game.transform.GetChild(0).Find("Helmet_LP").GetComponent<SkinnedMeshRenderer>().material;
		material.mainTexture = null;
		material.color = controller.teams[controller.currentTeam].teamColor;
		game.GetComponent<ValueChanger>().UpdateValues(speed.value, health.value, defense.value, strength.value,controller.currentTeam);
		return game;
    }
	public void ValuesChanged(){
		if (selectCanvas.activeSelf)
        {
            costOfNewUnit = 0;
            costOfNewUnit += Mathf.CeilToInt(speed.value * 4);
            costOfNewUnit += Mathf.CeilToInt(health.value * 4);
            costOfNewUnit += Mathf.CeilToInt(strength.value * 1);
            costOfNewUnit += Mathf.CeilToInt(defense.value * 1);
            cost.text = "Cost: " + costOfNewUnit.ToString();
        }
	}
	public void DoneCreatingUnit()
	{
		if(costOfNewUnit == 0){
			ValuesChanged();
		}
		if (controller.teams[controller.currentTeam].points >= costOfNewUnit)
		{
			
			selectCanvas.SetActive(false);
			readyUnit = true;
			controller.teams[controller.currentTeam].points -= costOfNewUnit;
		}
	}
	void DoneSpawningUnit()
	{
		readyUnit = false;

		if (turnController.NextTeamWithPoints(controller.currentTeam))
		{

             background.color = controller.teams[controller.currentTeam].teamColor;
			selectCanvas.SetActive(true);
			points.text = "Points left : " + controller.teams[controller.currentTeam].points;
		}else{
			playerPicker.canPickPlayer = true;
			Debug.Log("no points!");
		}
	}
}
