using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _health = 1;
    private int _strenght = 1;
    private int _speed = 1;
    private int _defense = 1;

    private int _expensiveCostModifier = 3;
    private int _cheapCostModifier = 2;

    private int _player1Points = 100;
    private int _player2Points = 100;

    private bool _player1Turn = true;

    public void AddHealthPoints(int health)
    {
        if ((_health == 10 && health > 0) || (_health == 1 && health < 0))
        {
            return;
        }

        if (_player1Turn)
        {
            _player1Points -= health * _expensiveCostModifier;
            if (_player1Points < 0)
            {
                _player1Points += health * _expensiveCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= health * _expensiveCostModifier;
            if (_player2Points < 0)
            {
                _player2Points += health * _expensiveCostModifier;
                return;
            }
        }

        _health += health;
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
            if (_player1Points < 0)
            {
                _player1Points += strenght * _cheapCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= strenght * _cheapCostModifier;
            if (_player2Points < 0)
            {
                _player2Points += strenght * _cheapCostModifier;
                return;
            }
        }

        _strenght += strenght;
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
            if (_player1Points < 0)
            {
                _player1Points += speed * _expensiveCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= speed * _expensiveCostModifier;
            if (_player2Points < 0)
            {
                _player2Points += speed * _expensiveCostModifier;
                return;
            }
        }

        _speed += speed;
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
            if (_player1Points < 0)
            {
                _player1Points += defense * _cheapCostModifier;
                return;
            }
        }
        else
        {
            _player2Points -= defense * _cheapCostModifier;
            if (_player2Points < 0)
            {
                _player2Points += defense * _cheapCostModifier;
                return;
            }
        }

        _defense += defense;
    }
}
