using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public enum Team
{
    red,
    blue
}

public class Unit : MonoBehaviour
{
    public Team myTeam;

    int _maxHealth;
    [SerializeField]int _health;
    [SerializeField]int _strength;
    [SerializeField]int _speed;
    [SerializeField]int _defense;

    public bool _isReady = false;
    bool _isAlive = true;
    bool _isFortied = false;
    Weapon _weapon;

    public GameObject setupUI;
    public GameObject combatUI;

    //Health, Strength, Speed, Defense
    string[] _statName = new string[4];
    public Slider[] statSliders; 
    public TMP_Text[] statText;
    public TMP_Text[] combatStatText;

    public GameObject canvasGameobject;

    UnitController _unitController;
    public Transform camPos;

    private void Start()
    {
        _statName[0] = "Health ";
        _statName[1] = "Strength ";
        _statName[2] = "Speed ";
        _statName[3] = "Defense ";

        canvasGameobject.SetActive(true);
        canvasGameobject.GetComponent<Canvas>().worldCamera = Camera.main;
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
        for (int i = 0; i < statSliders.Length; i++)
        {
            statText[i].text = _statName[i] + statSliders[i].value.ToString();
        }
    }

    // call from button
    public void AssignValues()
    {
        int unitCost = ((int)statSliders[0].value * 3) +
                       ((int)statSliders[1].value * 3) +
                       (int)statSliders[2].value +
                       (int)statSliders[3].value;

        if(_unitController.BuyUnit(unitCost))
        {
            canvasGameobject.SetActive(false);
            _isReady = true;

            _health = (int)statSliders[0].value;
            _maxHealth = (int)statSliders[0].value;
            _strength = (int)statSliders[1].value;
            _speed = (int)statSliders[2].value;
            _defense = (int)statSliders[3].value;

            canvasGameobject.SetActive(false);
            setupUI.SetActive(false);
            combatUI.SetActive(true);
            SetupCombatText();

            GameManager.instance.UpdatePoint(this._unitController);
            GameManager.instance.CheckIfEndTurn(this._unitController);
        }
        // TODO: Show player i can't be bought

    }

    public GameObject GetCanvas()
    {
        return canvasGameobject;
    }

    public void SetCharController(UnitController cc)
    {
        _unitController = cc;
    }

    public void SetupCombatText()
    {
        combatStatText[0].text = _statName[0] + _health + "/" + _maxHealth;
        combatStatText[1].text = _statName[1] + _strength;
        combatStatText[2].text = _statName[2] + _speed;
        combatStatText[3].text = _statName[3] + _defense;
    }

    public void TakeControl()
    {
        if(!_unitController._movedUnit)
        {
            // make this unit moveable
            StartCoroutine(CameraController.instance.LerpCamToUnit(0.5f, camPos));
        }
        else
        {
            // player already moved this turn so can't move again
        }
    }
}