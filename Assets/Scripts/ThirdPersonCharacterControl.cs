using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{

    public float strength;
    public float health;
    public float speed;
    public float defense;


	void Update ()
    {
        PlayerMovement();

        strength = GameInfo.Strength;
        health = GameInfo.Health;
        speed = GameInfo.Speed;
        defense = GameInfo.Defense;
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        Vector3 playerMovement = new Vector3(hor, 0f, ver) * speed * Time.deltaTime;
        transform.Translate(playerMovement, Space.Self);

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("ATTACK");
    }
}