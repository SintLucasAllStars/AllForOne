using UnityEngine;

[System.Serializable]
public class Player
{
    private const int minPoints = 10;

    [SerializeField] private int points = 100;
    [HideInInspector] public bool isOutOfPoints = false;

    private int charactersAlive;

    public bool CheckPoints(int costPoints)
    {
        return points >= costPoints;
    }

    public void CreateCharacter(int costPoints)
    {
        if(CheckPoints(costPoints))
        {
            charactersAlive++;
            points -= costPoints;

            isOutOfPoints = points < minPoints;
        }
    }

    public bool KillCharacter()
    {
        charactersAlive--;
        return charactersAlive <= 0;
    }

}
