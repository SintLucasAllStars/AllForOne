using UnityEngine;

[System.Serializable]
public class Player
{
    private const int minPoints = 10;

    public int playerIndex;

    public int points = 100;
    [HideInInspector] public bool isOutOfPoints = false;

    public int charactersAlive { get; private set; }

    public Color color;

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
