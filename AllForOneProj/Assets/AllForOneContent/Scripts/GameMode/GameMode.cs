using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowState
{
	Round_Buy,
	Round_Select,
	Round_ConfirmSelection,
	Round_Fight
}

public enum Team
{
	Red,
	Blue
}


public class GameMode : MonoBehaviour
{
	public static FlowState currentFlowState;

	public static Team currentTeam;

	public static float fightTime = 10;

    // Start is called before the first frame update
    void Start()
    {
		StartMatch();
    }


	public static void SetFlowState(FlowState newFlow)
	{
		currentFlowState = newFlow;
		
	}

	public void StartMatch()
	{
		SetFlowState(FlowState.Round_Select);
		currentTeam = Team.Red;
		
	}

	public static void SwitchCurrentTeam()
	{
		if(currentTeam == Team.Red)
		{
			currentTeam = Team.Blue;
		}
		else
		{
			currentTeam = Team.Red;
		}
	}



	
}
