using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;
	
	private Vector3 _restPos;
	private Quaternion _restRot;
	private Camera _camera;

	public Coroutine CurrentRoutine { get; private set; }


	[SerializeField] private float TopDownSpeed;
	
	public CameraState CurrentState { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_camera = GetComponent<Camera>();
		
		_restPos = transform.position;
		_restRot = transform.rotation;

		CurrentState = CameraState.Static;
	}

	private void Update()
	{
		switch (CurrentState)
		{
			case CameraState.TopDown:
				_camera.cullingMask |= 1 << LayerMask.NameToLayer("TeamMarkers");
				Vector3 movement = new Vector3(Input.GetAxis("Vertical"),0, -Input.GetAxis("Horizontal")) * TopDownSpeed;
				transform.position += movement;
				break;
			case CameraState.ThirdPerson:
				_camera.cullingMask &=  ~(1 << LayerMask.NameToLayer("TeamMarkers"));
				return;
			case CameraState.Static:
				return;
			default:
				return;
		}
	}

	public void ParentTo(Transform parentTrans)
	{
		transform.SetParent(parentTrans);
	}

	public void ClearParent()
	{
		transform.SetParent(null);
	}

	public void MoveCameraToPosition(float time, Vector3 position, Quaternion rotation)
	{
		if(CurrentRoutine != null) StopCoroutine(CurrentRoutine);
		CurrentRoutine = StartCoroutine(MoveToPosition(time, position, rotation));
	}

	public void ResetCameraPosition(float time)
	{
		if(CurrentRoutine != null) StopCoroutine(CurrentRoutine);
		CurrentRoutine = StartCoroutine(MoveToPosition(time, _restPos, _restRot));
	}

	public void SetCameraState(CameraState state)
	{
		CurrentState = state;
	}

	private IEnumerator MoveToPosition(float time, Vector3 position, Quaternion rotation)
	{
		Vector3 startPos = transform.position;
		Quaternion startRot = transform.localRotation;
		for (int i = 0; i < time; i++)
		{
			float t = (1 / time) * i;
			transform.position = Vector3.Lerp(startPos, position, t);
			transform.localRotation = Quaternion.Lerp(startRot, rotation , t);
			yield return new WaitForEndOfFrame();
		}

		CurrentRoutine = null;
	}
}

public enum CameraState
{
	TopDown,
	ThirdPerson,
	Static,
}