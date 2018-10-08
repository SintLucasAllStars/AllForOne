using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicker : MonoBehaviour {
	public bool canPickPlayer;//when true you can click on a character
	public GameObject pickObject; //object which will spawn on the characters
	public LayerMask layersToRayWith;

	List<GameObject> clickObjects = new List<GameObject>();//all object which spawned on the players 
	GameController gameController;
	bool setup = false;

	// Use this for initialization
	void Start () {
		gameController = GameController.Instance;
	}
	void SetPlayerPicker(){
		gameController.thirdPersonCamera.enabled = false;
        gameController.topDownViewCamera.enabled = true;
        if (clickObjects.Count > 0)
        {
            foreach (GameObject g in clickObjects)
            {
                Destroy(g);
            }
            clickObjects.Clear();
        }
        for (int i = 0; i < gameController.teams[gameController.currentTeam].teamUnits.Count; i++)
        {
            GameObject game = Instantiate(pickObject, gameController.teams[gameController.currentTeam].teamUnits[i].unit.transform.position, Quaternion.identity);
            game.GetComponent<PlayerPickObject>().id = i;
			game.GetComponent<MeshRenderer>().material.color = gameController.teams[gameController.currentTeam].teamColor;
            clickObjects.Add(game);
        }
	}
	// Update is called once per frame
	void Update () {
		if (canPickPlayer)
		{

			if(!setup){
				setup = true;
				SetPlayerPicker();
			}

			if (Input.GetMouseButtonDown(0))
			{
				RaycastHit hit;
				//Ray ray = .ScreenPointToRay(Input.mousePosition);
				Ray ray = gameController.topDownViewCamera.ScreenPointToRay(Input.mousePosition);
				//Debug.DrawRay(startRayPoint, -transform.right, Color.green, rayDistance, false);
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, layersToRayWith))
				{
					if (hit.transform.tag == "PickObject")
					{
						gameController.PlayerClicked(hit.transform.gameObject.GetComponent<PlayerPickObject>().id);
						canPickPlayer = false;
						gameController.pickingPlayer = false;
						setup = false;
					}
				}
			}
		}
	}
}
