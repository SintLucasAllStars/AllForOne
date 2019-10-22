﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicFever
{
    public class CharacterCreationManager : Singleton<CharacterCreationManager>
    {
        [SerializeField]
        private string[] _types = null;

        private int _health = 0, _strength = 0, _speed = 0, _defense = 0;

        public void AddHealth(int i)
        {
            _health = Mathf.Clamp(_health + i, 1, 10);
            UpdateValues();
        }
        public void AddStrength(int i)
        {
            _strength = Mathf.Clamp(_strength + i, 1, 10);
            UpdateValues();
        }
        public void AddSpeed(int i)
        {
            _speed = Mathf.Clamp(_speed + i, 1, 10);
            UpdateValues();
        }
        public void AddDefense(int i)
        {
            _defense = Mathf.Clamp(_defense + i, 1, 10);
            UpdateValues();
        }

        private string _type = "Overlord";

        [SerializeField]
        private TextMeshProUGUI _priceText = null, _healthValueText = null, _strengthValueText = null, _speedValueText = null, _defenseValueText = null;
        [SerializeField]
        private Slider _healthSlider = null, _strengthSlider = null, _speedSlider = null, _defenseSlider = null;

        private GameObject _showCaseUnit = null;

        [SerializeField]
        private Transform _showCaseContainer = null;

        private void Start() => SetRandomValues();

        public void SetRandomValues()
        {
            UpdateValues(
                Mathf.RoundToInt(Random.Range(_healthSlider.minValue + (_healthSlider.maxValue / 4), _healthSlider.maxValue - (_healthSlider.maxValue / 4))),
                Mathf.RoundToInt(Random.Range(_strengthSlider.minValue + (_strengthSlider.maxValue / 4), _strengthSlider.maxValue - (_strengthSlider.maxValue / 4))),
                Mathf.RoundToInt(Random.Range(_speedSlider.minValue + (_speedSlider.maxValue / 4), _speedSlider.maxValue - (_speedSlider.maxValue / 4))),
                Mathf.RoundToInt(Random.Range(_defenseSlider.minValue + (_defenseSlider.maxValue / 4), _defenseSlider.maxValue - (_defenseSlider.maxValue / 4))), _types[Random.Range(0, _types.Length)]);
        }

        public void UpdateValues() => UpdateValues(_health, _strength, _speed, _defense, _type);

        public void UpdateValues(int health, int strength, int speed, int defense, string type)
        {
            _health = health;
            _strength = strength;
            _speed = speed;
            _defense = defense;
            _type = type;

            UpdatePlayerUnit();
            UpdateText();
            SpawnUnit();
            SetSliderValues(_health, _strength, _speed, _defense);
        }

        private void SpawnUnit()
        {
            if (_showCaseUnit)
                Destroy(_showCaseUnit);

            _showCaseUnit = Instantiate(Resources.Load<GameObject>(_type), _showCaseContainer);
        }

        private void SetSliderValues(int health, int strength, int speed, int defense)
        {
            _healthSlider.value = health;
            _strengthSlider.value = strength;
            _speedSlider.value = speed;
            _defenseSlider.value = defense;
        }

        private void UpdatePlayerUnit()
        {
            int price = CalculateUnitPrice(_health, _strength, _speed, _defense);
            Player.Instance.SetPlayerUnit(new UnitData(_health, _strength, _speed, _defense, price, "", new Node(), _type, true, true, PlayerSide.Red));
        }

        private void UpdateText()
        {
            int price = CalculateUnitPrice(_health, _strength, _speed, _defense);
            _priceText.text = price.ToString();

            _healthValueText.text = _health.ToString();
            _strengthValueText.text = _strength.ToString();
            _speedValueText.text = _speed.ToString();
            _defenseValueText.text = _defense.ToString();
        }

        public int CalculateUnitPrice(int health, int strength, int speed, int defense) => (3 * health) + (3 * _speed) + (2 * strength) + (2 * defense);
    }
}