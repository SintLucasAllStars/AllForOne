using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
	[Header("Text")]
	public TMP_Text m_CurrentTeam;
	public TMP_Text m_CurrentMode;
	public TMP_Text m_CurrentTimer;
	public TMP_Text m_VictoryText;

	[Header("Base")]
	public Image m_CrossHair;
	public Button m_QuitButton;
	public PlayerController m_Pc;

	[Header("PowerUps")]
	public Image m_Rage;
	public Image m_Adrenalin;
	public Image m_TimeMachine;
	public GameObject m_PowerUps;
	

	// Start is called before the first frame update
	void Start()
	{
		m_QuitButton.gameObject.SetActive(false);
		m_VictoryText.enabled = false;
		m_CurrentTimer.SetText("Time Left: " + 10);
		m_CrossHair.enabled = false;
	}

	// Update is called once per frame
	void Update()
	{
		SetTeamText();
		SetModeText();
		SetTimerText();
		SetCrosshairEnabled();
		SetPowerUps();
		SetVicoryText();
	}

	void SetTeamText()
	{
		switch (GameMode.currentTeam)
		{
			case Team.Red:
				m_CurrentTeam.text = "Team: Red";
				break;
			case Team.Blue:
				m_CurrentTeam.text = "Team: Blue";
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
				m_CurrentMode.text = "Hire Units";
				break;
			case FlowState.Round_Select:
				m_CurrentMode.text = "Select a Unit";
				break;
			case FlowState.Round_Fight:
				m_CurrentMode.text = "";
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
				m_CurrentTimer.enabled = false;
				break;
			case FlowState.Round_Select:
				m_CurrentTimer.enabled = false;

				break;
			case FlowState.Round_Fight:
				m_CurrentTimer.enabled = true;
				m_CurrentTimer.text = "Time Left: " + m_Pc.countDown.ToString();

				break;
			default:
				break;
		}
	}

	void SetVicoryText()
	{
		if(GameMode.currentFlowState == FlowState.Round_End)
		{
			m_VictoryText.enabled = true;
			m_QuitButton.gameObject.SetActive(true);
			m_VictoryText.text = m_Pc.victoryTxt;
		}
	}

	void SetCrosshairEnabled()
	{
		switch (GameMode.currentFlowState)
		{
			case FlowState.Round_Buy:
				m_CrossHair.enabled = false;
				break;
			case FlowState.Round_Select:
				m_CrossHair.enabled = false;
				break;
			case FlowState.Round_Fight:
				m_CrossHair.enabled = true;
				break;
			default:
				break;
		}
	}

	void SetPowerUps()
	{
		if (GameMode.currentFlowState == FlowState.Round_Fight)
		{
			m_PowerUps.SetActive(true);
			if (m_Pc.possessedCharacter.currentPowerUps[2] != null)
			{
				m_Adrenalin.sprite = m_Pc.possessedCharacter.currentPowerUps[2].icon;
				m_Adrenalin.color = new Color(255, 255, 255, 255);
			}
			else
			{
				m_Adrenalin.sprite = null;
				m_Adrenalin.color = new Color(255, 255, 255, .25f);
			}
			if (m_Pc.possessedCharacter.currentPowerUps[1] != null)
			{
				m_Rage.sprite = m_Pc.possessedCharacter.currentPowerUps[1].icon;
				m_Rage.color = new Color(255, 255, 255, 255);
			}
			else
			{
				m_Rage.sprite = null;
				m_Rage.color = new Color(255, 255, 255, .25f);
			}
			if (m_Pc.possessedCharacter.currentPowerUps[0] != null)
			{
				m_TimeMachine.sprite = m_Pc.possessedCharacter.currentPowerUps[0].icon;
				m_TimeMachine.color = new Color(255, 255, 255, 255);
			}
			else
			{
				m_TimeMachine.sprite = null;
				m_TimeMachine.color = new Color(255, 255, 255, .25f);
			}
		}
		else
		{
			m_PowerUps.SetActive(false);
		}
	}
}
