using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public static CameraController Instance;
	
	private Vector3 _restPos;
	private Quaternion _restRot;

	private Coroutine _currentRoutine;

	[SerializeField] private float TopDownSpeed;
	
	public CameraState CurrentState { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		_restPos = transform.position;
		_restRot = transform.rotation;

		CurrentState = CameraState.Static;
	}

	private void Update()
	{
		switch (CurrentState)
		{
			case CameraState.TopDown:
				Vector3 movement = new Vector3(Input.GetAxis("Vertical"),0, -Input.GetAxis("Horizontal")) * TopDownSpeed;
				transform.position += movement;
				break;
			case CameraState.ThirdPerson:
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
		if(_currentRoutine != null) StopCoroutine(_currentRoutine);
		_currentRoutine = StartCoroutine(MoveToPosition(time, position, rotation));
	}

	public void ResetCameraPosition(float time)
	{
		if(_currentRoutine != null) StopCoroutine(_currentRoutine);
		_currentRoutine = StartCoroutine(MoveToPosition(time, _restPos, _restRot));
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
	}
	
}

public enum CameraState
{
	TopDown,
	ThirdPerson,
	Static,
}