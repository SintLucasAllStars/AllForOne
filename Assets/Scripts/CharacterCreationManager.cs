using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MechanicFever
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
            UpdateValues(Mathf.RoundToInt(Random.Range(_healthSlider.minValue + (_healthSlider.maxValue / 4), _healthSlider.maxValue - (_healthSlider.maxValue / 4))),
                Mathf.RoundToInt(Random.Range(_strengthSlider.minValue + (_strengthSlider.maxValue / 4), _strengthSlider.maxValue - (_strengthSlider.maxValue / 4))),
                Mathf.RoundToInt(Random.Range(_speedSlider.minValue + (_speedSlider.maxValue / 4), _speedSlider.maxValue - (_speedSlider.maxValue / 4))),
                Mathf.RoundToInt(Random.Range(_defenseSlider.minValue + (_defenseSlider.maxValue / 4), _defenseSlider.maxValue - (_defenseSlider.maxValue / 4))));
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
            int price = CalculateUnitPrice(_health, _strength, _speed, _defense);
            Player.Instance.SetPlayerUnit(new UnitData("", new Node(), "", true, true, PlayerSide.Red, _health, _strength, _speed, _defense, price));
        }

        private void UpdateText()
        {
            int price = CalculateUnitPrice(_health, _strength, _speed, _defense);
            _priceText.text = price.ToString();
        }

        public int CalculateUnitPrice(int health, int strength, int speed, int defense)
        {
            return (3 * health) + (3 * _speed) + (2 * strength) + (2 * defense);
        }
    }

}