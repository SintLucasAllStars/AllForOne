using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Rigidbody rb;
    private Transform camTransform;

    private int m_Health;
    private int m_Strength;
    private int m_Defence;
    private int m_Speed;
    private Team m_Team;

    private Vector3 unitPos;

    public void SetUp(int health, int strength, int defence, int speed, Team team)
    {
        m_Health = health;
        m_Strength = strength;
        m_Defence = defence;
        m_Speed = speed;
        m_Team = team;
        unitPos = transform.position;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        camTransform = GetComponentInChildren<Transform>();

        SetUp(30, 10, 10, 10, Team.Red); //<-toRemove

        unitPos = transform.position;
    }

    public void Move(Vector3 moveDir)
    {
        unitPos += moveDir * (m_Speed * Time.deltaTime);
        unitPos.y = transform.position.y;
        unitPos = transform.TransformDirection(unitPos);
        rb.MovePosition(unitPos);
    }

    public Vector3 GetCameraPos()
    {
        Vector3 pos = transform.GetChild(0).position - (transform.forward * 5.0f);
        return pos;
    }

    public void Look(Vector3 rotation)
    {
        rb.rotation = Quaternion.Euler(rotation);
    }
}

public enum Team { Red, Blue }