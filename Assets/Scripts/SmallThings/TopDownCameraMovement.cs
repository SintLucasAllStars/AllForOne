using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCameraMovement : MonoBehaviour
{
	[SerializeField]
	float minX, maxX, minZ, maxZ;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (GameController.Instance.pickingPlayer)
		{
			float x = InputManager.Instance.horizontalAxis;
			float z = InputManager.Instance.verticalAxis;
			Vector3 currentPos = transform.position;
			if (currentPos.x > maxX && x > 0)
			{
				x = 0;
			}
			if (currentPos.x < minX && x < 0)
			{
				x = 0;
			}
			if (currentPos.z > maxZ && z > 0)
			{
				z = 0;
			}
			if (currentPos.z < minZ && z < 0)
			{
				z = 0;
			}
			transform.Translate(x, 0, z, Space.World);

		}
	}
}
