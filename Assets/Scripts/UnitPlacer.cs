using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlacer : MonoBehaviour
{
	public static UnitPlacer Instance;

	[SerializeField] private GameObject _placeObject;
	private Material _placeObjectMaterial;
	
	private GameObject _currentObject;
	private bool _isPlacing = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_placeObjectMaterial = _placeObject.GetComponent<MeshRenderer>().material;
	}

	public void PlaceObject(GameObject obj)
	{
		_currentObject = obj;
		_isPlacing = true;
	}

	private void Update()
	{
		if(!_isPlacing) return;
		
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			_placeObject.transform.position = hit.point;
			if (hit.collider.CompareTag("PlaceAble"))
			{
				_placeObjectMaterial.color = Color.green;
				if (Input.GetMouseButtonDown(0))
				{
					CameraController.Instance.SetCameraState(CameraState.Static);
					_currentObject.transform.position = hit.point;
					_isPlacing = false;
					UnitCreation.Instance.gameObject.SetActive(true);
					UnitCreation.Instance.LoadScreen();
				}
			}
			else
			{
				_placeObjectMaterial.color = Color.red;
			}
		}
	}
}
