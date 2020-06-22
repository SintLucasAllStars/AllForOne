using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlowState
{
	Round_Buy,
	Round_Select,
	Round_ConfirmSelection,
	Round_Fight,
	Round_End
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

	public static List<GameObject> m_TeamBlue = new List<GameObject>();
	public static List<GameObject> m_TeamRed = new List<GameObject>();

	// Start is called before the first frame update
	void Start()
    {
		StartMatch();
		
		m_TeamRed.AddRange(GameObject.FindGameObjectsWithTag("Red"));
		m_TeamBlue.AddRange(GameObject.FindGameObjectsWithTag("Blue"));
		
		Debug.Log(m_TeamRed.Count);
		Debug.Log(m_TeamBlue.Count);
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
