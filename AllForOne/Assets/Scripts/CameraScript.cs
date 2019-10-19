using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private float _minClampX;

    [SerializeField]
    private float _maxClampX;

    [SerializeField]
    private float _zoomSpeed;

    [SerializeField]
    private float _minZoom;

    [SerializeField]
    private float _maxZoom;

    private float _xRotation = 80f;
    private float _yRotation = 0f;

    private void Update()
    {
        OnClick();

        CameraMovement();
        CameraZoom();
    }

    public void OnClick()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))
            {
                if (hit.collider.CompareTag("Ground") && GameManager._instance._canSpawn)
                {
                    GameManager._instance.CreateUnit(hit.point);
                }

                if (hit.collider.CompareTag("Player") && GameManager._instance._canSelect)
                {
                    GameManager._instance.ControlUnit(hit.collider.gameObject);
                }
            }
        }
    }

    private void CameraMovement()
    {
        float xInput = Input.GetAxis("Vertical");
        float yInput = Input.GetAxis("Horizontal");

        _xRotation += xInput;
        _yRotation += yInput;

        _xRotation = Mathf.Clamp(_xRotation, _minClampX, _maxClampX);

        transform.eulerAngles = new Vector3(_xRotation, -_yRotation, 0);
    }

    private void CameraZoom()
    {
        if (Input.GetKey(KeyCode.Equals) && _camera.fieldOfView > _minZoom)
        {
            _camera.fieldOfView -= _zoomSpeed;
        }

        if (Input.GetKey(KeyCode.Minus) && _camera.fieldOfView < _maxZoom)
        {
            _camera.fieldOfView += _zoomSpeed;
        }
    }
}
