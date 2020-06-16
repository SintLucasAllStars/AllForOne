using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public enum Team { red, blue }

public class Unit : MonoBehaviour
{
    public Team myTeam;

    [SerializeField]int _health;
    [SerializeField]int _strength;
    [SerializeField]int _speed;
    [SerializeField]int _defense;

    public bool _isReady = false;

    bool _isAlive = true;
    bool _isFortied;
    Weapon _weapon;

    public GameObject setupUI;
    //Health, Strength, Speed, Defense
    string[] _statName = new string[4];
    public Slider[] _statSliders; 
    public TMP_Text[] _statText;

    public GameObject _canvasGameobject;

    CharacterController charController;

    private void Start()
    {
        _statName[0] = "Health ";
        _statName[1] = "Strength ";
        _statName[2] = "Speed ";
        _statName[3] = "Defense ";
    }

    public int Attack()
    {
        return _strength * _weapon.GetDamage();
    }

    public void TakeHit(int a_Damage)
    {
        if(_health - a_Damage > 0)
        {
            _health -= a_Damage;
        }
        else
        {
            _health = 0;
            _isAlive = false;
        }
    }

    public void UpdateStatText()
    {
        for (int i = 0; i < _statSliders.Length; i++)
        {
            _statText[i].text = _statName[i] + _statSliders[i].value.ToString();
        }
    }

    // call from button
    public void AssignValues()
    {
        int unitCost = ((int)_statSliders[0].value * 3) +
                       ((int)_statSliders[1].value * 3) +
                       (int)_statSliders[2].value +
                       (int)_statSliders[3].value;

        if(charController.BuyUnit(unitCost))
        {
            _canvasGameobject.SetActive(false);
            _isReady = true;

            _health = (int)_statSliders[0].value;
            _strength = (int)_statSliders[1].value;
            _speed = (int)_statSliders[2].value;
            _defense = (int)_statSliders[3].value;

            _canvasGameobject.SetActive(false);
            setupUI.SetActive(false);
        }
        // TODO: Show player i can't be bought

    }

    public GameObject GetCanvas()
    {
        return _canvasGameobject;
    }

    public void SetCharController(CharacterController cc)
    {
        charController = cc;
    }
}