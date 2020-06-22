using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class Unit : MonoBehaviour
{
    int Health;
    int Strenght;
    float Speed;
    int Defense;

    public float walkSpeed = 2;
    public float runSpeed = 6;

    float turnSmoothTime = 0.2f;
    float turnSmoothVel;

    float speedSmoothTime = 0.1f;
    float speedSmoothvel;
    float curSpeed;


    Vector2 UnitMovement; 

    public Unit(int a_Health, int a_Strenght, float a_Speed, int a_Defense)
    {
        this.Health = a_Health;
        this.Strenght = a_Strenght;
        this.Speed = a_Speed;
        this.Defense = a_Defense;
        
    }

    void Setup()
    {
        
    }

    public void Move(Camera camera)
    {
        Vector2 _input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 _inputDir = _input.normalized;

        if (_inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(_inputDir.x, _inputDir.y) * Mathf.Rad2Deg + camera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVel, turnSmoothTime);
        }

        bool run = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((run) ? runSpeed : walkSpeed) * _inputDir.magnitude;
        curSpeed = Mathf.SmoothDamp(curSpeed, targetSpeed, ref speedSmoothvel, speedSmoothTime);

        transform.Translate(transform.forward * curSpeed * Time.deltaTime, Space.World); 
    }

}
