using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPMovement : MonoBehaviour
{
    private Vector2 inputVec;
    private Vector2 lookDir;
    private Transform camTransform;
    private CharacterController cc;

    public float speed = 6f;
    public float maxAccel = 10f;
    public float friction;
    public float mouseSpeed = 0.3f;

    void Start() 
    { 
        Cursor.lockState = CursorLockMode.Locked;
        camTransform = GetComponentInChildren<Camera>().transform;
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        CameraMovement();
        PlayerMovement();
    }
    
    void CameraMovement()
    {
        transform.eulerAngles = new Vector2(0, lookDir.x);
        camTransform.eulerAngles = new Vector2(-lookDir.y, lookDir.x);
    }

    void PlayerMovement()
    {
        //Velocity = how the character controller moved. It is the power the character moves with after interacting with the world. Current velocity after slowdown.
        Vector3 velocity = cc.velocity;
        Vector3 wishDir = transform.forward * inputVec.y + transform.right * inputVec.x;

        //Calculate velocity (de truc)
        //When pressing (Up + Left or similar) the vector would go over 1f, this clamps it so this doesn't happen.
        wishDir /= Mathf.Max(wishDir.magnitude, 1f);
        velocity *= 1 - friction * Time.deltaTime;
        velocity += wishDir * speed * Time.deltaTime;
        

        //Apply movement
        cc.Move(velocity * Time.deltaTime);
    }

    public void OnMove(InputAction.CallbackContext value)
    {
        inputVec = value.ReadValue<Vector2>();
    }
    public void OnLook(InputAction.CallbackContext value)
    {
        lookDir += value.ReadValue<Vector2>() * mouseSpeed;
        Debug.Log(value.ReadValue<Vector2>());
        lookDir.y = Mathf.Clamp(lookDir.y, -90, 90);
    }
}
