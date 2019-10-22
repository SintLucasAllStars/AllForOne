using UnityEngine;

public class PowerUp
{
    public enum Type
    {
        Adrenaline,
        Rage,
        TimeMachine
    }
    
    private Type _type;
    private float _timeLeft;
    private bool _active, _valid;

    public PowerUp(Type type)
    {
        _type = type;
        _timeLeft = GetPowerUpTime(type);
        _active = false;
        _valid = true;
    }

    public void Update()
    {
        if (_active)
        {
            _timeLeft -= Time.deltaTime;

            if (_timeLeft < 0)
            {
                _active = false;
                _valid = false;
            }
        }
    }

    public bool IsValid()
    {
        return _valid;
    }

    public bool IsActive()
    {
        return _active;
    }

    public bool Activate()
    {
        if (!_valid)
        {
            return false;
        }

        _valid = false;
        _active = true;
        return true;
    }

    public Type GetType()
    {
        return _type;
    }

    private static int GetPowerUpTime(Type type)
    {
        switch (type)
        {
            case Type.Adrenaline:
                return 10;
            case Type.Rage:
                return 5;
            case Type.TimeMachine:
                return 10;
            default:
                return -1;
        }
    }
    
}