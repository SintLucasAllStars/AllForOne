using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Character possessedCharacter;
	private Character currentSelection;
	public CameraManager cameraManager;
   
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
		cameraManager.SetViewMode();
		GameMode.SetFlowState(FlowState.Round_Select);
	}

	private void Update()
    {
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
                    Ray ray = cameraManager.playerCamera.ScreenPointToRay(Input.mousePosition);
                    if (Physics.Raycast(ray, out hit))
                    {
                        // if Character is Hit && Character equals players team
                        if (hit.collider.gameObject.GetComponent<Character>())
                        {
							// Possess Character
							currentSelection = hit.collider.gameObject.GetComponent<Character>();
							if(currentSelection.team == GameMode.currentTeam)
							{
								
								cameraManager.playerCamera.orthographic = false;
								cameraManager.SetCameraTarget(currentSelection.camSelectTarget);		
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
				cameraManager.SetViewMode();
				GameMode.SetFlowState(FlowState.Round_Select);
				GameMode.SwitchCurrentTeam();
                Cursor.visible = true;
                
            }
        }
    }

	

	IEnumerator fightTimer()
	{
		
		yield return new WaitForSeconds(GameMode.fightTime);
		UnPossess();
		cameraManager.SetViewMode();
		GameMode.SetFlowState(FlowState.Round_Select);
		GameMode.SwitchCurrentTeam();
		Cursor.visible = true;
	}
}


