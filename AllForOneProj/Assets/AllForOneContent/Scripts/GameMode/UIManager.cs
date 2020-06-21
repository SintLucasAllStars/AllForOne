using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
	public TMP_Text currentTeam;
	public TMP_Text currentMode;
	public TMP_Text currentTimer;
	public Image crossHair;

	bool countDownStarted;

	// Start is called before the first frame update
	void Start()
	{
		countDownStarted = false;
		currentTimer.SetText("Time Left: " + 10);
		crossHair.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		SetTeamText();
		SetModeText();
		SetTimerText();
		SetCrosshairEnabled();
	}

	void SetTeamText()
	{
		switch (GameMode.currentTeam)
		{
			case Team.Red:
				currentTeam.text = "Team: Red";
				break;
			case Team.Blue:
				currentTeam.text = "Team: Blue";
				break;
			default:
				break;
		}
	}

	void SetModeText()
	{
		switch (GameMode.currentFlowState)
		{
			case FlowState.Round_Buy:
				currentMode.text = "Hire Units";
				break;
			case FlowState.Round_Select:
				currentMode.text = "Select a Unit";
				break;
			case FlowState.Round_Fight:
				currentMode.text = "";
				break;
			default:
				break;
		}
	}

	void SetTimerText()
	{
		switch (GameMode.currentFlowState)
		{
			case FlowState.Round_Buy:
				currentTimer.enabled = false;
				break;
			case FlowState.Round_Select:
				currentTimer.enabled = false;

				break;
			case FlowState.Round_Fight:
				if (!countDownStarted)
				{
					currentTimer.enabled = true;
					StartCoroutine(CountDown());
				}

				break;
			default:
				break;
		}
	}

	void SetCrosshairEnabled()
	{
		switch (GameMode.currentFlowState)
		{
			case FlowState.Round_Buy:
				crossHair.enabled = false;
				break;
			case FlowState.Round_Select:
				crossHair.enabled = false;
				break;
			case FlowState.Round_Fight:
				crossHair.enabled = true;
				break;
			default:
				break;
		}
	}

	IEnumerator CountDown()
	{
		countDownStarted = true;
		float countDown = 10;
		do
		{	
			currentTimer.SetText("Time Left: " + countDown);
			yield return new WaitForSeconds(1);
			countDown--;
		}
		while (countDown > 0);
		countDownStarted = false;
		countDown = 10;
	}
}
