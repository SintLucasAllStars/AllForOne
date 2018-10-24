using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnController : MonoBehaviour
{
	[Tooltip("Time in seconds")]
	public float timePerRound = 5;

	public Text timerText;
	PickUpsController pickUpsController;
	float timer;
	// Use this for initialization
	void Start()
	{
		GameController.Instance.playerPicked += PlayerPicked;
		pickUpsController = GetComponent<PickUpsController>();
		timerText.text = "";
	}

	public void PlayerPicked()
	{
		timer = 0;
		StartCoroutine(Timer());
		timerText.text = timePerRound.ToString();
	}
	public void NextTeam()
	{
		if (GameController.Instance.teams.Count - 1 > GameController.Instance.currentTeam)
		{
			pickUpsController.SpawnRandomObject();
			GameController.Instance.currentTeam++;
		}
		else
		{
			pickUpsController.SpawnRandomObject();
			GameController.Instance.currentTeam = 0;
		}
	}
	public bool NextTeamWithPoints(int currentTeamId)
	{
		for (int i = currentTeamId + 1; i < GameController.Instance.teams.Count; i++)
		{
			if (GameController.Instance.teams[i].points > 10)
			{
				GameController.Instance.currentTeam = i;
				return true;
			}
		}
		for (int j = 0; j < currentTeamId + 1; j++)
		{
			if (GameController.Instance.teams[j].points > 10)
			{
				GameController.Instance.currentTeam = j;
				return true;
			}
		}
		NextTeam();
		return false;
	}
	IEnumerator Timer()
	{
		while (timer < timePerRound)
		{
			yield return new WaitForSeconds(timePerRound / timePerRound);
			timer++;
			timerText.text = (timePerRound - timer).ToString();
		}
		if (GameController.Instance.teams.Count > 1)
		{
			if(GameController.Instance.currentPlayer.outside){
				GameController.Instance.currentPlayer.health = 0;
				GameController.Instance.currentPlayer.dead = true;
			}
			timerText.text = "";
			NextTeam();
			GameController.Instance.pickingPlayer = true;
		}

	}
}
