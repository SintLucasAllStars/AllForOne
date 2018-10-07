using UnityEngine;

[System.Serializable]
public class Player
{
    [SerializeField] private int points;

    private int charactersAlive;

    public bool CheckPoints(int costPoints)
    {
        return points >= costPoints;
    }

    public bool CreateCharacter(int costPoints)
    {
        if(CheckPoints(costPoints))
        {
            charactersAlive++;
            points -= costPoints;
            return true;
        }
        return false;
    }

    public bool KillCharacter()
    {
        charactersAlive--;
        return charactersAlive <= 0;
    }

}
