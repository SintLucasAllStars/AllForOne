public class PowerUp
{
    public int duration;

    public int value;
    public TypeOfPowerUp typeOfPowerUp;

    public PowerUp(TypeOfPowerUp _typeOfPowerUp, int _duration, int _value)
    {
        this.typeOfPowerUp = _typeOfPowerUp;
        this.duration = _duration;
        this.value = _value;
    }
    public enum TypeOfPowerUp
    {
        speed,
        strength,
        counter
    }
}