using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : Singleton<GameController>
{
    public CharacterController currentPlayer;   // current Player Who Can Be controlled
	public List<Team> teams;
	public Camera thirdPersonCamera;
	public Camera topDownViewCamera;
	public Text winText;
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
		if(pickingPlayer&&teams.Count > 1){
			if (currentPlayer != null)
			{
				if(teams[currentTeam].teamUnits.Count == 0){
					teams.RemoveAt(currentTeam);
					return;
				}
				currentPlayer.lockPlayer = true;
				playerPicker.canPickPlayer = true;

			}

		}
		else
        {
			if (teams.Count == 1)
			{
				currentPlayer.lockPlayer = true;
				winText.text = "team: " + teams[0].teamName + " Wins";
				Debug.Log("team: " + teams[0].teamName + " Wins");
			}
        }
	
        if(InputManager.Instance.escButton){
            Application.Quit();
        }
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
