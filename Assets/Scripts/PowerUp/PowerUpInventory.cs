using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpInventory : MonoBehaviour 
{

    private readonly List<PowerUp> _powerUps = new List<PowerUp>();

    private int _currentAmountOfAdrenaline = 0;
    private int _currentAmountOfTimeMachine = 0;
    private int _currentAmountOfRage = 0;

    
    
    public void AddPowerUp(PowerUp powerUp)
    {
        _powerUps.Add(powerUp);

        switch (powerUp.MyPowerUp)
        {
            case PowerUp.PowerUpEnum.Adrenaline:
                _currentAmountOfAdrenaline++;
                break;
            case PowerUp.PowerUpEnum.Rage:
                _currentAmountOfRage++;
                break;
            case PowerUp.PowerUpEnum.TimeMachine:
                _currentAmountOfTimeMachine++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void RemovePowerUp(PowerUp powerUp)
    {
        switch (powerUp.MyPowerUp)
        {
            case PowerUp.PowerUpEnum.Adrenaline:
                _currentAmountOfAdrenaline--;
                if (_currentAmountOfAdrenaline < 0)
                {
                    _currentAmountOfAdrenaline = 0;
                }
                break;
            case PowerUp.PowerUpEnum.Rage:
                _currentAmountOfRage--;
                if (_currentAmountOfRage < 0)
                {
                    _currentAmountOfRage = 0;
                }
                break;
            case PowerUp.PowerUpEnum.TimeMachine:
                _currentAmountOfTimeMachine--;
                if (_currentAmountOfTimeMachine < 0)
                {
                    _currentAmountOfTimeMachine = 0;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _powerUps.Remove(powerUp);

    }
    
    public void ActivatePowerUp(PowerUp.PowerUpEnum powerUpEnum)
    {
        foreach (var thing in _powerUps)
        {
            if (thing.MyPowerUp != powerUpEnum) continue;
            GameManager.Instance.ActivatePowerUp(thing);
            RemovePowerUp(thing);
            GameManager.Instance.ResetPowerUpPanelTexts();
            break;
        }
        
    }

 
    public int GetCurrentAmountOfAdrenaline()
    {
        return _currentAmountOfAdrenaline;
    }

    public int GetCurrentAmountOfTimeMachine()
    {
        return _currentAmountOfTimeMachine;
    }

    public int GetCurrentAmountOfRage()
    {
        return _currentAmountOfRage;
    }
    


    
    

}
