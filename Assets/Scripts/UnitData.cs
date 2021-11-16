using System.Collections.Generic;

public class UnitData
{
    private Dictionary<string, float> _stats = new Dictionary<string, float>()
    {
        { "health", 0 },
        { "strength", 0 },
        { "speed", 0 },
        { "defence", 0 }
    };

    public float Health { get { return _stats["health"]; } set { _stats["health"] = value; } }
    public int Strength { get { return (int)_stats["strength"]; } }
    public int Speed { get { return (int)_stats["speed"]; } }
    public int Defence { get { return (int)_stats["defence"]; } }
    
    public UnitData(int health, int strength, int speed, int defence)
    {
        _stats["health"] = health;
        _stats["strength"] = strength;
        _stats["speed"] = speed;
        _stats["defence"] = defence;
    }
}
