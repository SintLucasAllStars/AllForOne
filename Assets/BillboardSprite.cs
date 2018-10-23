using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSprite : MonoBehaviour
{

	private Transform _cameraTransform;
	
	void Start ()
	{
		_cameraTransform = Camera.main.transform;
	}
	
	void Update () {
		transform.LookAt(_cameraTransform.position);
	}
}
