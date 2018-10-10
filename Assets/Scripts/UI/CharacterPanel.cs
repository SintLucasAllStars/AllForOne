using System;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private PanelText _panelTexts;
    [SerializeField] private Sliders _sliders;

    private float _speed = 1;
    private float _defence = 1;
    private float _health = 1;
    private float _strength = 1;

    private float _speedPercentage;
    private float _defencePercentage;
    private float _healthPercentage;
    private float _strengthPercentage;

    private float _speedCost;
    private float _defenceCost;
    private float _healthCost;
    private float _strengthCost;
    private float _pointsLeft;


    private float _originalPoints = 100;

    [SerializeField] private Button _hireButton;


    //Temp
    private CameraMovement _cameraMovement;

    [SerializeField] private Animator _animator;

    [SerializeField] private GameObject _showCaseObject;


    private enum ValuesEnum
    {
        Speed = 0,
        Defence = 1,
        Health = 2,
        Strength = 3
    }

    private Dictionary<ValuesEnum, Slider> _sliderDictionary = new Dictionary<ValuesEnum, Slider>();

    public void Start()
    {
        _animator.SetBool("MoveIn", true);
        _cameraMovement = Camera.main.gameObject.GetComponent<CameraMovement>();
        InitializeCost();
        InitializePercentage();
        InitializeDictionary();
        SetCurrentPlayerText();
    }

    private void InitializeDictionary()
    {
        _sliderDictionary.Add(ValuesEnum.Speed, _sliders.SpeedSlider);
        _sliderDictionary.Add(ValuesEnum.Defence, _sliders.DefenceSlider);
        _sliderDictionary.Add(ValuesEnum.Health, _sliders.HealthSlider);
        _sliderDictionary.Add(ValuesEnum.Strength, _sliders.StrengthSlider);
        _panelTexts.Costs.text = Convert.ToInt32(GetTotalValue()).ToString();
    }

    private void InitializePercentage()
    {
        _speedPercentage = (25f - _speedCost) / 99f;
        _defencePercentage = (25f - _defenceCost) / 99f;
        _healthPercentage = (25f - _healthCost) / 99f;
        _strengthPercentage = (25f - _healthCost) / 99f;
    }

    private void InitializeCost()
    {
        _speedCost = PlayerManager.Instance.SpeedCost;
        _speed = _speedCost;
        _defenceCost = PlayerManager.Instance.DefenceCost;
        _defence = _defenceCost;
        _healthCost = PlayerManager.Instance.HealthCost;
        _health = _healthCost;
        _strengthCost = PlayerManager.Instance.StrengthCost;
        _strength = _strengthCost;
        _pointsLeft = _originalPoints - GetTotalValue();
        _panelTexts.PointsLeft.text = _pointsLeft.ToString();
    }

    private float GetCost(int enumValue)
    {
        switch ((ValuesEnum) enumValue)
        {
            case ValuesEnum.Speed:
                return _speedCost;
            case ValuesEnum.Defence:
                return _defenceCost;
            case ValuesEnum.Health:
                return _healthCost;
            case ValuesEnum.Strength:
                return _strengthCost;
            default:
                throw new ArgumentOutOfRangeException("enumValue", enumValue, null);
        }
    }

    private void ResetSliders()
    {
        for (int i = 0; i < _sliderDictionary.Count; i++)
        {
            _sliderDictionary[(ValuesEnum) i].value = _sliderDictionary[(ValuesEnum) i].minValue;
        }
    }

    private void SetCost(int enumValue, float value)
    {
        switch ((ValuesEnum) enumValue)
        {
            case ValuesEnum.Speed:
                _speed = _speedCost + (value * _speedPercentage);
                _panelTexts.Speed.text = value.ToString();
                break;
            case ValuesEnum.Defence:
                _defence = _defenceCost + value * _defencePercentage;
                _panelTexts.Defence.text = value.ToString();
                break;
            case ValuesEnum.Health:
                _health = _healthCost + value * _healthPercentage;
                _panelTexts.Health.text = value.ToString();
                break;
            case ValuesEnum.Strength:
                _strength = _strengthCost + value * _strengthPercentage;
                _panelTexts.Strength.text = value.ToString();
                break;
            default:
                throw new ArgumentOutOfRangeException("enumValue", enumValue, null);
        }

        _panelTexts.Costs.text = GetTotalValue().ToString("0.00");
        _pointsLeft = _originalPoints - GetTotalValue();  
        _panelTexts.PointsLeft.text = _pointsLeft.ToString("0.00");
        _hireButton.interactable = !(_pointsLeft <= 0);
    }


    public void OnNewCharacter()
    {
        _animator.SetBool("MoveIn", true);
        _showCaseObject.SetActive(true);
        _cameraMovement.CameraSlerp(_cameraMovement.CharacterView, false);
        InitializeCost();
        ResetSliders();
        _pointsLeft =  PlayerManager.Instance.GetCurrentlyActivePlayer().GetPoints() - GetTotalValue();
        _originalPoints = PlayerManager.Instance.GetCurrentlyActivePlayer().GetPoints();
        
        _panelTexts.PointsLeft.text = _pointsLeft.ToString("0.00");
        if (_pointsLeft <= 0)
        {
            _hireButton.interactable = false;
        }
        SetCurrentPlayerText();
    }


    private void SetCurrentPlayerText()
    {
        Debug.Log(PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber);
        _panelTexts.CurrentPlayer.text = "Player " + PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber + " Turn";
    }
    
    public void OnDone()
    {
        _showCaseObject.SetActive(false);
        PlayerManager.Instance.GetCurrentlyActivePlayer().SetPoints((int)_pointsLeft);
        _animator.SetBool("MoveIn", false);    
        AddCharacter();
    }

    public float GetTotalValue()
    {
        return _speed + _defence + _health + _strength;
    }


    public void OnSliderValueChanged(int enumValue)
    {
        float currentValue = _sliderDictionary[(ValuesEnum) enumValue].value;
        SetCost(enumValue, currentValue);
    }


    public void AddCharacter()
    {
        PlayerManager.Instance.AddCharacterToCurrentPlayerAndInstansiate(_strength, _defence, _speed, _health, PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber);
        _cameraMovement.CameraSlerp(_cameraMovement.TopView, false);
    }



}

[Serializable]
public class PanelText
{
    public Text Speed;
    public Text Defence;
    public Text Strength;
    public Text Health;
    public Text Costs;
    public Text PointsLeft;
    public Text CurrentPlayer;
}

[Serializable]
public class Sliders
{
    public Slider SpeedSlider;
    public Slider DefenceSlider;
    public Slider StrengthSlider;
    public Slider HealthSlider;
}