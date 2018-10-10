using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelector : MonoBehaviour
{

	private UnitController _currentUnit;
	private GameObject _camera;
	private Vector3 _restPos;
	private Quaternion _restRot;
	
	void Start ()
	{
		_camera = Camera.main.gameObject;
		_restPos = _camera.transform.position;
		_restRot = _camera.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) ClearCurrentUnit();

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
			{
				Debug.Log("ray");
				if (hit.collider.CompareTag("Unit"))
				{
					Debug.Log(hit);
					SetCurrentUnit(hit.transform.gameObject);
				}
			}
		}
	}

	private void SetCurrentUnit(GameObject unit)
	{
		_currentUnit = unit.GetComponent<UnitController>();
		_currentUnit.enabled = true;
		_camera.transform.SetParent(unit.transform);
		_camera.transform.position = unit.transform.position - (unit.transform.forward*2) + (Vector3.up * 3);
		_camera.transform.localRotation = Quaternion.Euler(Vector3.zero);
	}

	private void ClearCurrentUnit()
	{
		_currentUnit.enabled = false;
		_currentUnit = null;
		_camera.transform.position = _restPos;
		_camera.transform.eulerAngles = new Vector3(90,0,0);
		
	}
}
