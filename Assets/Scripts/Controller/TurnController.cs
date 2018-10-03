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
	IEnumerator Timer(){
		yield return new WaitForSeconds(timePerRound);
		NextTeam();
		GameController.Instance.pickingPlayer = true;
	}
}
