using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	public Dictionary<GameObject, Units> units = new Dictionary<GameObject, Units>();

	public TextMeshProUGUI m_Text;

	public enum PLAYER { PLAYER1, PLAYER2 };

	public PLAYER player;
	public LayerMask m_Mask;

	public Slider m_HealthSlider, m_StrengthSlider, m_SpeedSlider, m_DefenseSlider;

	public TextMeshProUGUI m_HealthText, m_StrengthText, m_SpeedText, m_DefenseText, m_PriceText, m_PlayerIndicator, m_Currency;
	public Canvas m_Canvas, m_UnitStats;

	public GameObject m_Unit1, m_Unit2;

	[SerializeField]
	private float m_PlayerCurrency = 100;

	[SerializeField]
	private float m_Player2Currency = 100;

	private bool m_UnitPlaced = false;
	public bool m_Selected = false;

	static public Transform m_TrSelect = null;
	private Camera m_Camera;

	// Start is called before the first frame update
	private void Start()
	{
		m_Camera = Camera.main;
		player = PLAYER.PLAYER1;
	}

	// Update is called once per frame
	private void Update()
	{
		Run();

		if (m_PlayerCurrency <= 0 && m_Player2Currency <= 0)
		{
			m_Canvas.gameObject.SetActive(false);
			m_UnitPlaced = true;
		}
	}

	public void ToggleUnitStats()
	{
		m_UnitStats.gameObject.SetActive(true);
	}

	public void Run()
	{
		UpdateText();
		ChangePlayer();

		if (!m_UnitPlaced && !m_Canvas.gameObject.activeSelf)
		{
			PlaceUnit();
		}
	}

	public void CreateCharacter()
	{
		switch (player)
		{
			case PLAYER.PLAYER1:
				if (m_PlayerCurrency > 0 && m_PlayerCurrency >= CalculatePrice())
				{
					Units unit = new Units(m_HealthSlider.value, m_StrengthSlider.value, m_SpeedSlider.value, m_DefenseSlider.value);
					m_Canvas.gameObject.SetActive(false);
					m_UnitPlaced = false;
				}
				break;

			case PLAYER.PLAYER2:
				if (m_Player2Currency > 0 && m_Player2Currency >= CalculatePrice())
				{
					Units unit = new Units(m_HealthSlider.value, m_StrengthSlider.value, m_SpeedSlider.value, m_DefenseSlider.value);
					m_Canvas.gameObject.SetActive(false);
					m_UnitPlaced = false;
				}
				break;

			default:
				break;
		}
	}

	private void UpdateText()
	{
		m_HealthText.text = string.Format("Health : {0}", m_HealthSlider.value);
		m_StrengthText.text = string.Format("Strength : {0}", m_StrengthSlider.value);
		m_SpeedText.text = string.Format("Speed : {0}", m_SpeedSlider.value);
		m_DefenseText.text = string.Format("Defence : {0}", m_DefenseSlider.value);
		m_PriceText.text = string.Format("Price : {0}", CalculatePrice());
	}

	private Units UnitCreation(float a_HealthSlider, float a_StrengthSlider, float a_SpeedSlider, float a_DefenseSlider)
	{
		Units unit = new Units(a_HealthSlider, a_StrengthSlider, a_SpeedSlider, a_DefenseSlider);
		return unit;
	}

	private float CalculatePrice()
	{
		float m_Health = m_HealthSlider.value, m_Strength = m_StrengthSlider.value, m_Speed = m_SpeedSlider.value, m_Defense = m_DefenseSlider.value;
		float m_TotalPrice = m_Health + m_Strength + m_Speed + m_Defense;

		return m_TotalPrice;
	}

	private void ChangePlayer()
	{
		var m_Player = m_PlayerIndicator;
		switch (player)
		{
			case PLAYER.PLAYER1:

				m_Player.text = "PLAYER 1";
				m_Player.color = Color.red;
				m_Currency.text = "Points : " + m_PlayerCurrency.ToString();
				m_Currency.color = Color.red;
				m_PriceText.color = Color.red;

				break;

			case PLAYER.PLAYER2:

				m_Player.text = "PLAYER 2";
				m_Player.color = Color.blue;
				m_Currency.text = "Points : " + m_Player2Currency.ToString();
				m_Currency.color = Color.blue;
				m_PriceText.color = Color.blue;
				break;

			default:
				break;
		}
	}

	private void PlaceUnit()
	{
		m_UnitPlaced = false;
		RaycastHit hit;
		Vector3 offset = new Vector3(0, 0.5f, 0);

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit, m_Mask))
		{
			if (hit.transform.gameObject.CompareTag("Placeable"))
			{
				if (Input.GetMouseButtonDown(0))
				{
					switch (player)
					{
						case PLAYER.PLAYER1:
							GameObject m_RedUnit = Instantiate(m_Unit2, hit.point + offset, Quaternion.identity);
							units.Add(m_RedUnit, UnitCreation(m_HealthSlider.value, m_StrengthSlider.value,
								m_SpeedSlider.value, m_DefenseSlider.value));
							UpdateCurrency(CalculatePrice());
							if (m_Player2Currency > 0)
							{
								player = PLAYER.PLAYER2;
							}
							break;

						case PLAYER.PLAYER2:
							GameObject m_BlueUnit = Instantiate(m_Unit1, hit.point + offset, Quaternion.identity);
							units.Add(m_BlueUnit, UnitCreation(m_HealthSlider.value, m_StrengthSlider.value,
								m_SpeedSlider.value, m_DefenseSlider.value));
							UpdateCurrency(CalculatePrice());
							if (m_PlayerCurrency > 0)
							{
								player = PLAYER.PLAYER1;
							}
							break;

						default:
							break;
					}

					m_UnitPlaced = true;
					CanvasToggle();
				}
			}
		}
	}

	public void UpdateSelectedUnit(Transform a_SelectedUnit)
	{
		m_TrSelect = a_SelectedUnit;
	}

	public Transform ReturnTransform()
	{
		return m_TrSelect;
	}

	public void UpdateCurrency(float a_TotalPrice)
	{
		switch (player)
		{
			case PLAYER.PLAYER1:
				m_PlayerCurrency -= a_TotalPrice;
				break;

			case PLAYER.PLAYER2:
				m_Player2Currency -= a_TotalPrice;
				break;

			default:
				break;
		}
	}

	private void CanvasToggle()
	{
		if (m_Canvas.gameObject.activeInHierarchy)
		{
			m_Canvas.gameObject.SetActive(false);
		}
		else
		{
			ResetSliders();
			m_Canvas.gameObject.SetActive(true);
		}
	}

	private void ResetSliders()
	{
		m_HealthSlider.value = 0;
		m_StrengthSlider.value = 0;
		m_SpeedSlider.value = 0;
		m_DefenseSlider.value = 0;
	}

	public void SkipTurn()
	{
		switch (player)
		{
			case PLAYER.PLAYER1:
				player = PLAYER.PLAYER2;
				break;

			case PLAYER.PLAYER2:
				player = PLAYER.PLAYER1;
				break;

			default:
				break;
		}
	}
}