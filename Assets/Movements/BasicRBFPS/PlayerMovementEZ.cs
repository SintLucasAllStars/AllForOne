using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementEZ : MonoBehaviour
{
    private Vector2 inputVec;
    public float movementSpeed = 10;

    void FixedUpdate()
    {
        transform.Translate(inputVec.x * Time.deltaTime * movementSpeed, 0, inputVec.y * Time.deltaTime * movementSpeed);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        inputVec = value.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext value)
    {
        float inputSpace = value.ReadValue<float>();
        //jump = inputSpace != 0 ? true : false;
    }
}
