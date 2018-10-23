using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{

	private UnitController _currentUnit;
	private Quaternion _restRot;
	private CameraController _cameraController;

	private bool _canSelect;
	private int _currentPlayer;
	
	void Start ()
	{
		_cameraController = CameraController.Instance;
	}

	public void SelectUnit(int player)
	{
		Debug.Log("Select: " + player);
		_currentPlayer = player;
		_canSelect = true;
	}

	void Update () {
		if(!_canSelect) return;
		if(Input.GetKeyDown(KeyCode.Escape)) ClearCurrentUnit();

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				if (hit.collider.CompareTag("Unit"))
				{
					if(hit.transform.GetComponent<Unit>().Player != _currentPlayer) return;
					SetCurrentUnit(hit.transform.gameObject);
				}
			}
		}
	}

	private void SetCurrentUnit(GameObject unit)
	{
		_currentUnit = unit.GetComponent<UnitController>();
		_currentUnit.enabled = true;
		_cameraController.ParentTo(unit.transform);
		_cameraController.SetCameraState(CameraState.ThirdPerson);
		_cameraController.MoveCameraToPosition(100, unit.transform.position - (unit.transform.forward * 2) + (Vector3.up * 3),Quaternion.Euler(Vector3.zero));
		RoundManager.Instance.SelectedUnit(_currentUnit.Unit);
	}



	public void ClearCurrentUnit()
	{
		_currentUnit.enabled = false;
		_currentUnit = null;
		_cameraController.ClearParent();
		_cameraController.SetCameraState(CameraState.TopDown);
		_cameraController.ResetCameraPosition(100);
	}
}
