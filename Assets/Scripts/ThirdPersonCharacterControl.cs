using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{

    public float strength;
    public float health;
    public float speed;
    public float defense;

    public Camera unitCam;

    public Collider col;

    private bool isControlled;

    private float rotationspeed = 100f;

    void Start()
    {
        strength = GameInfo.Strength;
        health = GameInfo.Health;
        speed = GameInfo.Speed;
        defense = GameInfo.Defense;
    }

    void Update()
    {
        if (isControlled)
        {
            PlayerMovement();
            StartCoroutine(SwitchTime());
        } 

        if (Input.GetKey(KeyCode.P))
        {
            unitCam.enabled = false;
            isControlled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.red);
            Debug.Log("click");
        }
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 playerMovement = new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }
        float rotation = Input.GetAxis("Horizontal") * rotationspeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);
    }

    public void ControlUnit()
    {
        unitCam.enabled = true;
        col.enabled = true;
        isControlled = true;
    }

    void Attack()
    {
        //Debug.Log("ATTACK");
    }

    void Test()
    {
        unitCam.enabled = false;
        col.enabled = false;
        isControlled = false;
    }

    IEnumerator SwitchTime()
    {
        yield return new WaitForSeconds(10f);
        unitCam.enabled = false;
        isControlled = false;
        Debug.Log("TURN ENDED");
        yield return null;
    }
}