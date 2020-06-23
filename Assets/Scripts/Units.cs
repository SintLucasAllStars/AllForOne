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

	public float GetHealth()
	{
		return m_Health;
	}

	public GameObject GetPrefab()
	{
		return prefab;
	}

	public float GetStrength()
	{
		return m_Strength;
	}

	public float GetSpeed()
	{
		return m_Speed;
	}

	public float GetDefense()
	{
		return m_Defense;
	}

	public float GetPrice()
	{
		return m_Price;
	}

	public Units()
	{
		this.m_Health = Random.Range(1, 10);
		this.m_Strength = Random.Range(1, 10);
		this.m_Speed = Random.Range(1, 10);
		this.m_Defense = Random.Range(1, 10);
		CaculatePrice();
	}

	public Units(float a_Health, float a_Strength, float a_Speed, float a_Defense)
	{
		this.m_Health = a_Health;
		this.m_Strength = a_Strength;
		this.m_Speed = a_Speed;
		this.m_Defense = a_Defense;
		CaculatePrice();
	}

	public void CaculatePrice()
	{
		float price = m_Health + m_Strength + m_Speed + m_Defense;
		m_Price = price;
	}
}