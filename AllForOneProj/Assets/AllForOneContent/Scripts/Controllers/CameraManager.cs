using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewState
{
	TopDown3D,
	TopDownOrtho
}


public class CameraManager : MonoBehaviour
{
    public Camera playerCamera;
    public float positionSpeed = 1;
    public float rotationSpeed = 1;
	public float TPPositionSpeed;
	public float TPRotationSpeed;

	public ViewState currentViewState;
    public Transform defaultTargetOrtho_Player1;
	
	public Transform defaultTarget3D_Player1;
	
    private Transform cameraTarget;

    // Start is called before the first frame update
    void Start()
    {
		SetViewMode();
    }

	// Update is called once per frame
	void Update()
	{
		InterpToTarget();
		CameraMovement();

		if (Input.GetKeyDown(KeyCode.Tab) && GameMode.currentFlowState == FlowState.Round_Buy || Input.GetKeyDown(KeyCode.Tab) && GameMode.currentFlowState == FlowState.Round_Select)
		{ 
			if (currentViewState == ViewState.TopDownOrtho)
			{
				currentViewState = ViewState.TopDown3D;
				SetViewMode();
			}
			else
			{
				currentViewState = ViewState.TopDownOrtho;
				SetViewMode();
			}
		}

    }

    private void InterpToTarget()
    {
		
		if (GameMode.currentFlowState == FlowState.Round_Fight)
		{
				playerCamera.transform.position = cameraTarget.transform.position;
				playerCamera.transform.rotation = cameraTarget.transform.rotation;
		}
		else
		{
			float curPosSpeed = positionSpeed;
			float curRotSpeed = rotationSpeed;
			playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, cameraTarget.position, Time.deltaTime * positionSpeed);
			playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation, cameraTarget.rotation, Time.deltaTime * rotationSpeed);
		}
        
    }

    public void SetCameraTarget(Transform t)
    {
        cameraTarget = t;
    }

    public void SetViewMode()
	{
		switch (currentViewState)
		{
			case ViewState.TopDown3D:
				playerCamera.orthographic = false;
					SetCameraTarget(defaultTarget3D_Player1);
				break;
			case ViewState.TopDownOrtho:
				playerCamera.orthographic = true;
				SetCameraTarget(defaultTargetOrtho_Player1);
				break;
			default:
				break;
		}

	}

	void CameraMovement()
	{
		if (GameMode.currentFlowState == FlowState.Round_Select || GameMode.currentFlowState == FlowState.Round_Buy)
		{
			if (Input.GetMouseButton(1))
			{
				float mouseX = Input.GetAxis("Mouse X");
				float mouseY = Input.GetAxis("Mouse Y");
				cameraTarget.position += new Vector3(-mouseX, 0, -mouseY);
			}
		}
	}
}
