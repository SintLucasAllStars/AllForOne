using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager cameraManager;

    private void Awake()
    {
        cameraManager = this;
    }

    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform cameraHolder;

    [SerializeField] public Transform overviewCameraPosition;

    #region The varibles for moving the camera to the next target.
    [SerializeField] private int destinationPositionSpeed;
    [SerializeField] private int destinationRotationSpeed;

    private Vector3 destinationPosition;
    private Quaternion destinationRotation;
    #endregion

    [SerializeField] private bool cameraSwitching = false;


    private void Start()
    {
        //MoveCamera(overviewCameraPosition);
    }

    /// <summary>
    /// Move the Camera to the specific unit.
    /// </summary>
    public void ChangeUnit(Unit unit)
    {
        cameraHolder.SetParent(unit.cameraTransform);
        //ZeroCameraPosition(cameraTransform);
        CameraMoveTowards(unit.cameraTransform.localPosition, unit.cameraTransform.localRotation);
    }

    /// <summary>
    /// Move the Camera to a specific Transform.
    /// </summary>
    public void MoveCamera(Transform transform)
    {
        cameraHolder.SetParent(transform);
        //ZeroCameraPosition(cameraTransform);
        CameraMoveTowards(transform.localPosition, transform.localRotation);
    }

    /// <summary>
    /// Set the Camera position and rotation to 0 on all axis.
    /// </summary>
    private void ZeroCameraPosition(Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    /// <summary>
    /// Returns the localPosition that the camera will move towards.
    /// </summary>
    private void CameraMoveTowards(Vector3 localPosition, Quaternion localRotation)
    {
        destinationPosition = localPosition;
        destinationRotation = localRotation;
        cameraSwitching = true;
    }

    private void FixedUpdate()
    {
        if (cameraSwitching)
        {
            float stepPos = destinationPositionSpeed * Time.deltaTime;
            float stepRot = destinationRotationSpeed * Time.deltaTime;

            cameraHolder.localPosition = Vector3.MoveTowards(cameraHolder.localPosition, Vector3.zero, stepPos);
            cameraHolder.localRotation = Quaternion.RotateTowards(cameraHolder.localRotation, Quaternion.Euler(Vector3.zero), stepRot);

            if ((cameraTransform.parent.localPosition == destinationPosition) && (cameraTransform.parent.localRotation == destinationRotation))
            {
                cameraSwitching = false;
            }
        }
    }
}