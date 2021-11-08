using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 inputVec = Vector2.zero;

    void Start()
    {
        
    }

    void Update()
    {
        
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
