public class PowerUp
{
    public int duration;
    public string name;
    public TypeOfPowerUp typeOfPowerUp;

    public PowerUp(string _name, int _duration)
    {
        this.duration = _duration;
        this.name = _name;
    }
    public enum TypeOfPowerUp
    {
        speed,
        strength,
        counter
    }
}