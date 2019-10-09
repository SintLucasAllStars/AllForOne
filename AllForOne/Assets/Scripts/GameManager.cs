using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    [SerializeField]
    private Unit _unit;

    [SerializeField]
    private Text _unitCostText;

    [SerializeField]
    private Text _playerCurrencyText;

    [SerializeField]
    private Text _healthText;

    [SerializeField]
    private Text _strenghtText;

    [SerializeField]
    private Text _speedText;

    [SerializeField]
    private Text _defenseText;

    private int _health = 1;
    private int _strenght = 1;
    private int _speed = 1;
    private int _defense = 1;

    private int _expensiveCostModifier = 3;
    private int _cheapCostModifier = 2;

    private int _player1Points = 100;
    private int _player2Points = 100;

    private int _unitCost;

    private bool _player1Turn = true;

    private void Awake()
    {
        _instance = this;
    }

    public void CreateUnit(Vector3 spawnPosition)
    {
        Unit unit = Instantiate(_unit, spawnPosition, transform.rotation);

        unit._isPlayer1 = _player1Turn;
        unit._health = _health;
        unit._strenght = _strenght;
        unit._speed = _speed;
        unit._defense = _defense;
    }

    public void AddHealthPoints(int health)
    {
        if ((_health == 10 && health > 0) || (_health == 1 && health < 0))
        {
            return;
        }

        if (_player1Turn)
        {
            _player1Points -= health * _expensiveCostModifier;
            _playerCurrencyText.text = "$" + _player1Points;
            if (_player1Points < 0)
            {
                _player1Points += health * _expensiveCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= health * _expensiveCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0)
            {
                _player2Points += health * _expensiveCostModifier;

                return;
            }
        }

        _unitCost += health * _expensiveCostModifier;
        _health += health;

        _healthText.text = _health.ToString();
        _unitCostText.text = "$" + _unitCost;
    }

    public void AddStrenghtPoints(int strenght)
    {
        if ((_strenght == 10 && strenght > 0) || (_strenght == 1 && strenght < 0))
        {
            return;
        }

        if (_player1Turn)
        {
            _player1Points -= strenght * _cheapCostModifier;
            _playerCurrencyText.text = "$" + _player1Points;
            if (_player1Points < 0)
            {
                _player1Points += strenght * _cheapCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= strenght * _cheapCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0)
            {
                _player2Points += strenght * _cheapCostModifier;
                return;
            }
        }

        _unitCost += strenght * _cheapCostModifier;
        _strenght += strenght;

        _strenghtText.text = _strenght.ToString();
        _unitCostText.text = "$" + _unitCost;
    }

    public void AddSpeedPoints(int speed)
    {
        if ((_speed == 10 && speed > 0) || (_speed == 1 && speed < 0))
        {
            return;
        }

        if (_player1Turn)
        {
            _player1Points -= speed * _expensiveCostModifier;
            _playerCurrencyText.text = "$" + _player1Points;
            if (_player1Points < 0)
            {
                _player1Points += speed * _expensiveCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= speed * _expensiveCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0)
            {
                _player2Points += speed * _expensiveCostModifier;
                return;
            }
        }

        _unitCost += speed * _expensiveCostModifier;
        _speed += speed;

        _speedText.text = _speed.ToString();
        _unitCostText.text = "$" + _unitCost;
    }

    public void AddDefensePoints(int defense)
    {
        if ((_defense == 10 && defense > 0) || (_defense == 1 && defense < 0))
        {
            return;
        }

        if (_player1Turn)
        {
            _player1Points -= defense * _cheapCostModifier;
            _playerCurrencyText.text = "$" + _player1Points;
            if (_player1Points < 0)
            {
                _player1Points += defense * _cheapCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= defense * _cheapCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0)
            {
                _player2Points += defense * _cheapCostModifier;
                return;
            }
        }
        _unitCost += defense * _cheapCostModifier;
        _defense += defense;

        _defenseText.text = _defense.ToString();
        _unitCostText.text = "$" + _unitCost;
    }
}
