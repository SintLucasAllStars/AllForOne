using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    public CharacterController currentPlayer;   // current Player Who Can Be controlled
	public Team[] teams;
	public Camera thirdPersonCamera;
	public Camera topDownViewCamera;

	public int currentTeam;

	[HideInInspector]
	public bool pickingPlayer;

	CameraFollowScript cameraFollowScript;
	PlayerPicker playerPicker;
	int oldPlayerIndex;

	public delegate void PlayerPicked();
	public PlayerPicked playerPicked;

    // Use this for initialization
    void Start()
    {
		//foreach(Team team in teams){
		//	Unit unit = new Unit();
		//	unit.unit = SpawnPlayer(Vector3.zero, team.teamPrefab).GetComponent<CharacterController>();
		//	team.teamUnits.Add(unit);
		//	unit = new Unit();
        //    unit.unit = SpawnPlayer(Vector3.zero, team.teamPrefab).GetComponent<CharacterController>();
        //    team.teamUnits.Add(unit);
		//}
		//currentPlayer = teams[0].teamUnits[0].unit;
		playerPicker = GetComponent<PlayerPicker>();
		cameraFollowScript = thirdPersonCamera.GetComponent<CameraFollowScript>();
		pickingPlayer = true;
		//SetNewPlayer(currentPlayer, null);
    }

    // Update is called once per frame
    void Update()
    {
		if(pickingPlayer){
			if (currentPlayer != null)
			{
				currentPlayer.lockPlayer = true;
				playerPicker.canPickPlayer = true;

			}
		}
	
        if(InputManager.Instance.escButton){
            Application.Quit();
        }
    }
	GameObject SpawnPlayer(Vector3 position,GameObject prefab){
		return Instantiate(prefab, position, Quaternion.identity);
	}
	void SetNewPlayer(CharacterController newPlayer,CharacterController oldPlayer){
		if (oldPlayer != null)
		{
			oldPlayer.lockPlayer = true;
		}
		newPlayer.lockPlayer = false;
		cameraFollowScript.target = newPlayer.gameObject.transform;
		currentPlayer = newPlayer;
		playerPicked();
	}
	public void PlayerClicked(int index){
		SetNewPlayer(teams[currentTeam].teamUnits[index].unit, currentPlayer);
		thirdPersonCamera.enabled = true;
        topDownViewCamera.enabled = false;
	}
}
