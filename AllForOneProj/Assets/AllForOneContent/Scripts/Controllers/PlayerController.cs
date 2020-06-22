using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	private Character currentSelection;

	[HideInInspector] public Character possessedCharacter;
	[HideInInspector] public bool timerPaused;
	[HideInInspector] public float countDown;
	[HideInInspector] public string victoryTxt;
	public CameraManager m_CameraManager;
	public ItemSpawner m_ItemSpawner;
	public GameObject m_Bridge;
	public Material m_BridgeOnMat;
	public Material m_BridgeOffMat;
	

    public void Possess(Character character)
    {
        if (possessedCharacter != null)
        {
            UnPossess();
        }
        possessedCharacter = character;
        possessedCharacter.isPossessed = true;
		possessedCharacter.M_Pc = this;
    }

    public void UnPossess()
    {
		if (possessedCharacter)
		{
			possessedCharacter.isPossessed = false;
			possessedCharacter.M_Pc = null;
			possessedCharacter = null;
		}
    }

	public void ConfirmSelection()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
		Possess(currentSelection);
		currentSelection.selectUI.SetActive(false);
		currentSelection = null;
		GameMode.SetFlowState(FlowState.Round_Fight);
		StartCoroutine(fightTimer());

	}

	public void CancelSelection()
	{
		currentSelection.selectUI.SetActive(false);
		currentSelection = null;
		m_CameraManager.SetViewMode();
		GameMode.SetFlowState(FlowState.Round_Select);
	}

	private void Update()
    {
		EndGameCheck();
		SwitchBridge();
		//Select/Buy Characters
		if (Input.GetMouseButtonDown(0) && possessedCharacter == null)
        {
            switch (GameMode.currentFlowState)
            {
                case FlowState.Round_Buy:
					Debug.Log("BuyMode");
					break;

                case FlowState.Round_Select:
					RaycastHit hit;
					
					Debug.Log("SelectMode");
					
					// Ray Cast to Mouse Screen Pos 
					Ray ray = m_CameraManager.playerCamera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        // if Character is Hit && Character equals players team
                        if (hit.collider.gameObject.GetComponent<Character>())
                        {
							// Possess Character
							currentSelection = hit.collider.gameObject.GetComponent<Character>();
							if(currentSelection.team == GameMode.currentTeam)
							{

								m_CameraManager.playerCamera.orthographic = false;
								m_CameraManager.SetCameraTarget(currentSelection.camSelectTarget);		
								GameMode.SetFlowState(FlowState.Round_ConfirmSelection);
								currentSelection.selectUI.GetComponent<StatUIUpdater>().UpdateText();
								currentSelection.selectUI.SetActive(true);	
							}
                        }
                    }
                    break;

                case FlowState.Round_Fight:
					
					Debug.Log("FightMode");
				
					break;
                default:
                    break;
            }
        }

		//End Player Controlled Round
        if(GameMode.currentFlowState == FlowState.Round_Fight)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                UnPossess();
				m_CameraManager.SetViewMode();
				GameMode.SetFlowState(FlowState.Round_Select);
				GameMode.SwitchCurrentTeam();
                Cursor.visible = true;
                
            }
        }
    }

	void EndGameCheck()
	{
		if (GameMode.m_TeamBlue.Count <= 0)
		{
			UnPossess();
			victoryTxt = "Team Red Has Won";
			GameMode.SetFlowState(FlowState.Round_End);
			Cursor.visible = true;
		}
		else if (GameMode.m_TeamRed.Count <= 0)
		{
			UnPossess();
			victoryTxt = "Team Blue has Won";
			GameMode.SetFlowState(FlowState.Round_End);
			Cursor.visible = true;
		}
	}

	void SwitchBridge()
	{
		switch (GameMode.currentFlowState)
		{
			case FlowState.Round_Buy:
				break;
			case FlowState.Round_Select:
				m_Bridge.GetComponent<MeshRenderer>().material = m_BridgeOffMat;
				m_Bridge.GetComponent<BoxCollider>().enabled = false;
				break;
			case FlowState.Round_ConfirmSelection:
				break;
			case FlowState.Round_Fight:
				m_Bridge.GetComponent<MeshRenderer>().material = m_BridgeOnMat;
				m_Bridge.GetComponent<BoxCollider>().enabled = true;
				break;
			case FlowState.Round_End:
				break;
			default:
				break;
		}
	}

	public void BackToMainMenu()
	{
		Application.Quit();
	}

	IEnumerator fightTimer()
	{
		countDown = GameMode.fightTime;
		do
		{
			yield return new WaitForSeconds(1);
			while (timerPaused)
			{
				yield return null;
			}
			countDown--;
		}
		while (countDown > 0);
		
		if(GameMode.currentFlowState != FlowState.Round_End)
		{ 
			UnPossess();
			m_CameraManager.SetViewMode();
			GameMode.SetFlowState(FlowState.Round_Select);
			GameMode.SwitchCurrentTeam();
			countDown = GameMode.fightTime;
			Cursor.visible = true;
			m_ItemSpawner.SpawnItem();
		}
		
	}
}


