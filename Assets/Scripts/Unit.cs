using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Rigidbody rb;
    private Transform camTransform;
    private Vector3 offset = new Vector3(0, 0, -5);

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
    }

    public void Move(Vector3 moveDir)
    {
        unitPos += moveDir * (m_Speed * Time.deltaTime);
        unitPos.y = transform.position.y;
        rb.MovePosition(unitPos);
    }

    public Vector3 GetCameraPos()
    {
        Vector3 transform = camTransform.position + offset;
        return transform;
    }

    public void Look(Vector3 rotation)
    {

    }
}

public enum Team { Red, Blue }