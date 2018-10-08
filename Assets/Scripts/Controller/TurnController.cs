using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {
	[Tooltip("Time in seconds")]
	public float timePerRound = 5;
	// Use this for initialization
	void Start () {
		GameController.Instance.playerPicked += PlayerPicked;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void PlayerPicked(){
		StartCoroutine(Timer());
	}
	public void NextTeam(){
		if (GameController.Instance.teams.Length - 1 > GameController.Instance.currentTeam)
        {
            GameController.Instance.currentTeam++;
        }
        else
        {
            GameController.Instance.currentTeam = 0;
        }
	}
	public bool NextTeamWithPoints(int currentTeamId){
		for (int i = currentTeamId+1; i < GameController.Instance.teams.Length;i++){
			if (GameController.Instance.teams[i].points > 10)
			{
				GameController.Instance.currentTeam = i;
				return true;
			}
		}
		for (int j = 0; j < currentTeamId+1; j++)
        {
			if (GameController.Instance.teams[j].points > 10)
			{
				GameController.Instance.currentTeam = j;
				return true;
			}
        }
		return false;
	}
	IEnumerator Timer(){
		yield return new WaitForSeconds(timePerRound);
		NextTeam();
		GameController.Instance.pickingPlayer = true;
	}
}
