using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonTarget : MonoBehaviour
{
    public float sensitivity;
    public Vector2 clamp;
    public Transform player;
    public Vector3 offset;

    Vector2 inputVec;
    Vector3 rotate;
    private void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        transform.position = player.position + offset;
    }

    public void OnLook(InputAction.CallbackContext value)
    {
        inputVec += value.ReadValue<Vector2>();
        /*transform.Rotate(inputVec.y * Time.deltaTime * sensitivity, inputVec.x * Time.deltaTime * sensitivity, 0);*/
        inputVec.y = Mathf.Clamp(inputVec.y, clamp.x * 10, clamp.y * 10);
        transform.rotation = Quaternion.Euler(inputVec.y * sensitivity, inputVec.x * sensitivity, 0);
    }
}
