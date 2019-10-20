using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private Transform overviewCameraPosition;

    #region The varibles for moving the camera to the next target.
    [SerializeField] private int destinationPositionSpeed;
    [SerializeField] private int destinationRotationSpeed;

    private Vector3 destinationPosition;
    private Quaternion destinationRotation;
    #endregion

    private bool cameraSwitching = false;


    private void Start()
    {
        OverviewCamera();
    }

    /// <summary>
    /// Move the Camera to the specific unit.
    /// </summary>
    public void ChangeUnit(Unit unit)
    {
        cameraTransform.localPosition = unit.cameraTransform.localPosition;
    }

    /// <summary>
    /// Move the Camera to a specific location.
    /// </summary>
    public void MoveCamera(Vector3 position, Vector3 rotation)
    {
        cameraTransform.localPosition = position;
        cameraTransform.localRotation = Quaternion.Euler(rotation);
    }

    /// <summary>
    /// Move the Main Camera above the Game.
    /// </summary>
    public void OverviewCamera()
    {
        cameraTransform.SetParent(overviewCameraPosition);
        CameraMoveTowards(cameraTransform.localPosition, cameraTransform.localRotation);
    }

    /// <summary>
    /// Set the Camera position and rotation to 0 on all axis.
    /// </summary>
    private void ZeroCameraPosition()
    {
        cameraTransform.localPosition = Vector3.zero;
        cameraTransform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    /// <summary>
    /// Returns the localPosition that the camera will move towards.
    /// </summary>
    private void CameraMoveTowards(Vector3 localPosition, Quaternion localRotation)
    {
        destinationPosition = localPosition;
        destinationRotation = localRotation;
    }


    private void Update()
    {
        if (cameraSwitching)
        {
            cameraTransform.parent.localPosition = Vector3.MoveTowards(cameraTransform.localPosition, destinationPosition, destinationPositionSpeed);
            cameraTransform.parent.localRotation = Quaternion.RotateTowards(cameraTransform.localRotation, destinationRotation, destinationRotationSpeed);
            
            if ((cameraTransform.parent.localPosition == destinationPosition) && (cameraTransform.parent.localRotation == destinationRotation))
            {
                cameraSwitching = false;
            }
        }
    }
}