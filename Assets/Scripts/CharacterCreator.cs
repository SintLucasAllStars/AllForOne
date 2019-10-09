using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreator : MonoBehaviour
{
    public GameObject _unit;
    public GameObject _UI;
    public Slider _healthSlider;
    public Slider _strengthSlider;
    public Slider _speedSlider;
    public Slider _defenseSlider;
    public int _maxSliderValue = 10;

    public Text _currentPlayer;
    public Text _totalCurrencyText;
    public Text _healthText;
    public Text _strengthText;
    public Text _speedText;
    public Text _defenseText;
    public Text _costText;

    private int _health;
    private int _strength;
    private int _speed;
    private int _defense;

    private int _cost;

    private bool _isPlacing = false;

    private void Start()
    {
        UpdateUIValues();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, Camera.main.ScreenPointToRay(Input.mousePosition).direction, Color.red, 50);

        if (Input.GetKeyDown(KeyCode.Mouse0) && _isPlacing)
        {
            Place();
        }
    }

    public void HealthChange(float value)
    {
        _health = Mathf.RoundToInt(value);
        CalculateNewCost();
    }

    public void StrengthChange(float value)
    {
        _strength = Mathf.RoundToInt(value);
        CalculateNewCost();
    }

    public void SpeedChange(float value)
    {
        _speed = Mathf.RoundToInt(value);
        CalculateNewCost();
    }

    public void DefenseChange(float value)
    {
        _defense = Mathf.RoundToInt(value);
        CalculateNewCost();
    }

    public void Hire()
    {
        if (_cost <= PlayerManager.Instance._activePlayer._currency)
        {
            PlayerManager.Instance._activePlayer._currency -= _cost;
            PlaceCharacter();
        }
    }

    private void CalculateNewCost()
    {
        int totalStats = _maxSliderValue * 2 + _maxSliderValue * 2 + _maxSliderValue + _maxSliderValue;
        float valueOfSelectedStats = _health * 2f + _strength * 2f + _speed + _defense;
        _cost = 10 + Mathf.RoundToInt(valueOfSelectedStats / totalStats * 90f);

        UpdateUIValues();
    }

    private void UpdateUIValues()
    {
        _currentPlayer.text = "Player " + PlayerManager.Instance._activePlayer._playerID.ToString();
        _totalCurrencyText.text = PlayerManager.Instance._activePlayer._currency.ToString();

        _healthText.text = "Health: " + _healthSlider.value.ToString();
        _strengthText.text = "Strength: " + _strengthSlider.value.ToString();
        _speedText.text = "Speed: " + _speedSlider.value.ToString();
        _defenseText.text = "Defense: " + _defenseSlider.value.ToString();

        _costText.text = "Cost: " + _cost.ToString();
    }

    private void PlaceCharacter()
    {
        _UI.SetActive(false);
        _isPlacing = true;
    }

    private void Place()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log(hit.collider.gameObject.tag);
            if (hit.collider.gameObject.CompareTag("Floor"))
            {
                GameObject newUnit = Instantiate(_unit, hit.point, Quaternion.identity);
                PlayableUnit thisUnit = newUnit.AddComponent<PlayableUnit>();
                AssignUnitValues(thisUnit);

                _isPlacing = false;
                _UI.SetActive(true);
                PlayerManager.Instance.CheckMoneyForRemainingMoney();
                //^vgm zorgt dit voor stack overflow als al het geld op is
                PlayerManager.Instance.EndTurn();
                UpdateUIValues();
            }
        }
    }

    private void AssignUnitValues(PlayableUnit thisUnit)
    {
        thisUnit._player = PlayerManager.Instance._activePlayer;
        thisUnit._health = _health;
        thisUnit._strength = _strength;
        thisUnit._speed = _speed;
        thisUnit._defense = _defense;
    }
}
