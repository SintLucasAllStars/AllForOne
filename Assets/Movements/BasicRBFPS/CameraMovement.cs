using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private Vector2 inputVec;
    [Range(0.0f, 1.0f)]
    public float mouseSensitivity = 0.1f;
    public Transform playerObj;
    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = inputVec.x * mouseSensitivity;
        float mouseY = inputVec.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerObj.Rotate(Vector3.up * mouseX);
    }
    public void OnLook(InputAction.CallbackContext value)
    {
        inputVec += value.ReadValue<Vector2>();
    }
}
