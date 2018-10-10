using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = InputManager.Instance.horizontalAxis;
		float z = InputManager.Instance.verticalAxis;

		transform.Translate(x, 0, z, Space.World);
	}
}
