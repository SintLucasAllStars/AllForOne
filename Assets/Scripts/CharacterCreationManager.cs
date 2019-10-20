using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace AllForOne
{
    public class CharacterCreationManager : Singleton<CharacterCreationManager>
    {
        private int _health, _strength, _speed, _defense;
        [SerializeField]
        private TextMeshProUGUI _priceText, _healthValueText, _strengthValueText, _speedValueText, _defenseValueText;
        [SerializeField]
        private Slider _healthSlider, _strengthSlider, _speedSlider, _defenseSlider;

        private void Start()
        {
            UpdateValues();
        }

        public void UpdateValues()
        {
            UpdateValues(Mathf.RoundToInt(_healthSlider.value), Mathf.RoundToInt(_strengthSlider.value), Mathf.RoundToInt(_speedSlider.value), Mathf.RoundToInt(_defenseSlider.value));
        }

        public void UpdateValues(int health, int strength, int speed, int defense)
        {
            _health = health;
            _strength = strength;
            _speed = speed;
            _defense = defense;

            UpdatePlayerUnit();
            UpdateText();
            SetSliderValues(_health, _strength, _speed, _defense);
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
            int price = Player.Instance.CalculateUnitPrice(_health, _strength, _speed, _defense);
            Player.Instance.SetPlayerUnit(new UnitData("", new Node(), "", true, true, PlayerSide.Blu, _health, _strength, _speed, _defense, price));
        }

        private void UpdateText()
        {
            int price = Player.Instance.CalculateUnitPrice(_health, _strength, _speed, _defense);
            _priceText.text = price.ToString();
        }
    }

}