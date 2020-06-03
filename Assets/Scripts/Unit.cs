using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Rigidbody rb;
    private Transform targetTransform;
    private float lookDirX;
    private float lookDirY;

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
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetTransform = transform.GetChild(0);

        SetUp(30, 10, 10, 250, Team.Red); //<-toRemove
    }

    public void Move(Vector3 moveDir)
    {
        unitPos = moveDir * (m_Speed * Time.fixedDeltaTime);
        unitPos = transform.TransformDirection(unitPos);
        rb.velocity = unitPos;
    }

    public Vector3 GetCameraPos()
    {
        Vector3 pos = targetTransform.position - (transform.forward * 5.0f);
        return pos;
    }

    public Transform GetTarget()
    {
        return targetTransform;
    }

    public void Look(float mouseX, float mouseY)
    {
        lookDirX += mouseX;
        lookDirY -= mouseY;
        lookDirY = Mathf.Clamp(lookDirY, -35, 60);
        rb.rotation = Quaternion.Euler(new Vector3(0, lookDirX, 0));
        targetTransform.rotation = Quaternion.Euler(new Vector3(lookDirY, lookDirX, 0));
    }
}

public enum Team { Red, Blue }