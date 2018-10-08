using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{

    [SerializeField] private PanelText _panelTexts;
    [SerializeField] private Sliders _sliders;

    private int _speed;
    private int _defence;
    private int _health;
    private int _strength;

    private int _speedPercentage;
    private int _defencePercentage;
    private int _healthPercentage;
    private int _strengthPercentage;

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
        
        
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        SetPercentageValues();
        _sliderDictionary.Add(ValuesEnum.Speed, _sliders.SpeedSlider);
        _sliderDictionary.Add(ValuesEnum.Defence, _sliders.DefenceSlider);
        _sliderDictionary.Add(ValuesEnum.Health, _sliders.HealthSlider);
        _sliderDictionary.Add(ValuesEnum.Strength, _sliders.StrengthSlider);
        _sliderDictionary[ValuesEnum.Speed].maxValue =  _speedPercentage 
        _sliderDictionary[ValuesEnum.Defence].maxValue = 
        _sliderDictionary[ValuesEnum.Strength].maxValue = Convert.ToInt32(100 / PlayerManager.Instance.StrengthCost);
        _sliderDictionary[ValuesEnum.Health].maxValue = Convert.ToInt32(100 / PlayerManager.Instance.HealthCost);
    }

    private void SetPercentageValues()
    {
        _speedPercentage = Convert.ToInt32(100 / PlayerManager.Instance.SpeedCost);
        _defencePercentage = Convert.ToInt32(100 / PlayerManager.Instance.DefenceCost);
        _healthPercentage = Convert.ToInt32(100 / PlayerManager.Instance.HealthCost);
        _strengthPercentage = Convert.ToInt32(100 / PlayerManager.Instance.StrengthCost);
    }
    

    public void OnNewCharacter()
    {
        
    }

    public void OnDone()
    {
        
    }

    public int GetTotalValue()
    {
        return _speed + _defence + _health + _strength;
    }

    public void OnSliderValueChanged(int enumValue)
    {
        float oldValue = _sliderDictionary[(ValuesEnum) enumValue].value;
        if(oldValue )
        
        
    
    }
    
    

    public void AddCharacter(string speed,string defence, string strength)
    {
      
        //PlayerManager.Instance.GetCurrentlyActivePlayer().AddCharacter(strengthNumber,defenceNumber,speedNumber);
    }
        
    
    
    public void SpeedValueChanged()
    {
        
    }

}

[Serializable]
public class PanelText
{
    public Text Speed;
    public Text Defence;
    public Text Strength;
    public Text PointsLeft;
    public Text Health;
}

[Serializable]
public class Sliders
{
    public Slider SpeedSlider;
    public Slider DefenceSlider;
    public Slider StrengthSlider;
    public Slider HealthSlider;

}
