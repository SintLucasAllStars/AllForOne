using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 inputVec = Vector2.zero;
    public float speed = 6.5f;

    void Update()
    {
        transform.Translate(inputVec.x * speed * Time.deltaTime, 0, inputVec.y * speed * Time.deltaTime);
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
