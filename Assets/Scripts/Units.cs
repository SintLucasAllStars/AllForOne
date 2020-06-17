using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class Units
{
    //Health
    private float m_Health;
    //Strength
    private float m_Strength;
    //Speed
    private float m_Speed;
    //Defense 
    private float m_Defense;
    private float m_Price;

    public GameObject prefab;


    public float getHealth()
    {
        return m_Health;
    }

    public GameObject getPrefab()
    {
        return prefab;
    }

    public float getStrength()
    {
        return m_Strength;
    }

    public float getSpeed()
    {
        return m_Speed;
    }

    public float getDefense()
    {
        return m_Defense;
    }

    public float getPrice()
    {
        return m_Price;
    }

    public Units()
    {
        this.m_Health = Random.Range(1, 10);
        this.m_Strength = Random.Range(1, 10);
        this.m_Speed = Random.Range(1, 10);
        this.m_Defense = Random.Range(1, 10);
        caculatePrice();
    }

    public Units(float a_Health, float a_Strength, float a_Speed, float a_Defense)
    {
        this.m_Health = a_Health;
        this.m_Strength = a_Strength;
        this.m_Speed = a_Speed;
        this.m_Defense = a_Defense;
        caculatePrice();
    }

    public void caculatePrice()
    {
        float price = m_Health + m_Strength + m_Speed + m_Defense;
        m_Price = price;
    }

    
}
