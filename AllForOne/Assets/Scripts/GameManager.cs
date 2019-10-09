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

    [SerializeField]
    private Image _colorBlock;

    [SerializeField]
    private Canvas _unitCanvas;

    [SerializeField]
    private Camera _mainCamera;

    private int _health = 1;
    private int _strenght = 1;
    private int _speed = 1;
    private int _defense = 1;

    private int _expensiveCostModifier = 3;
    private int _cheapCostModifier = 2;

    private int _player1Points = 90;
    private int _player2Points = 90;

    private int _unitCost = 10;

    private bool _player1Turn = true;

    public bool _canSpawn;

    private void Awake()
    {
        _instance = this;

        _unitCostText.text = "$" + _unitCost;
        _playerCurrencyText.text = "$" + _player1Points;

    }

    public void PlaceUnit()
    {
        if ((_player1Turn && _player1Points >= 0) || (!_player1Turn && _player2Points >= 0))
        {
            _canSpawn = true;
            _mainCamera.enabled = true;
            _unitCanvas.enabled = false;
        }
    }

    public void CreateUnit(Vector3 spawnPosition)
    {
        Unit unit = Instantiate(_unit, spawnPosition, transform.rotation);

        unit._isPlayer1 = _player1Turn;
        unit._health = _health;
        unit._strenght = _strenght;
        unit._speed = _speed;
        unit._defense = _defense;

        _canSpawn = false;
        _mainCamera.enabled = false;
        _unitCanvas.enabled = true;

        SwitchTeam();
    }

    private void SwitchTeam()
    {
        if (_player1Turn)
        {
            _player1Points -= 10;
            _player1Turn = false;
            _colorBlock.color = new Color32(42, 87, 226, 255);
            _playerCurrencyText.text = "$" + _player2Points;
        }
        else
        {
            _player2Points -= 10;
            _player1Turn = true;
            _colorBlock.color = new Color32(226, 42, 42, 255);
            _playerCurrencyText.text = "$" + _player1Points;
        }

        _health = 1;
        _healthText.text = _health.ToString();

        _strenght = 1;
        _strenghtText.text = _strenght.ToString();

        _speed = 1;
        _speedText.text = _speed.ToString();

        _defense = 1;
        _defenseText.text = _defense.ToString();

        _unitCost = 10;
        _unitCostText.text = "$" + _unitCost;
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
            if (_player1Points < 0 && health > 0)
            {
                _player1Points += health * _expensiveCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
                return;
            }
        }
        else
        {
            _player2Points -= health * _expensiveCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0 && health > 0)
            {
                _player2Points += health * _expensiveCostModifier;
                _playerCurrencyText.text = "$" + _player2Points;
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
            if (_player1Points < 0 && strenght > 0)
            {
                _player1Points += strenght * _cheapCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
                return;
            }
        }
        else
        {
            _player2Points -= strenght * _cheapCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0 && strenght > 0)
            {
                _player2Points += strenght * _cheapCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
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
            if (_player1Points < 0 && speed > 0)
            {
                _player1Points += speed * _expensiveCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
                return;
            }
        }
        else
        {
            _player2Points -= speed * _expensiveCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0 && speed > 0)
            {
                _player2Points += speed * _expensiveCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
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
            if (_player1Points < 0 && defense > 0)
            {
                _player1Points += defense * _cheapCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
                return;
            }
        }
        else
        {
            _player2Points -= defense * _cheapCostModifier;
            _playerCurrencyText.text = "$" + _player2Points;
            if (_player2Points < 0 && defense > 0)
            {
                _player2Points += defense * _cheapCostModifier;
                _playerCurrencyText.text = "$" + _player1Points;
                return;
            }
        }
        _unitCost += defense * _cheapCostModifier;
        _defense += defense;

        _defenseText.text = _defense.ToString();
        _unitCostText.text = "$" + _unitCost;
    }
}
