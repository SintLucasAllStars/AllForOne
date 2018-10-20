using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private UnitSelectionManager _unitSelectionManager;
    private UnitInterface _unitInterface;

    private Player[] _PlayerArr;

    private int _currentPlayingPlayer = 0;

    private void Start()
    {
        _unitSelectionManager = FindObjectOfType<UnitSelectionManager>();
        _unitInterface = FindObjectOfType<UnitInterface>();

        _PlayerArr = new[]{
            new Player(Color.red),
            new Player(Color.blue)
        };
    }

    public int GetCurrentPlayingPlayerIndex {
        get {
            return _currentPlayingPlayer;
        }
    }

    public void SetNextPlayerIndex()
    {
        _currentPlayingPlayer = _currentPlayingPlayer == 0 ? _currentPlayingPlayer += 1 : _currentPlayingPlayer -= 1;
    }

    public Player GetCurrentPlayer {
        get {
            return _PlayerArr[_currentPlayingPlayer];
        }
    }

    public void StartPlayerMovement(Unit unit)
    {
        StartCoroutine(LerpToUnit(unit.transform));
        unit.IsUnitActive = true;
    }

    public IEnumerator LerpToUnit(Transform unitTransform)
    {
        Camera.main.transform.SetParent(unitTransform);

        Vector3 target = new Vector3(0, unitTransform.localPosition.y, -2);

        while (Vector3.Distance(Camera.main.transform.localPosition, target) >= 0.01f)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, target, Time.deltaTime * 3);
            Camera.main.transform.LookAt(unitTransform);
            yield return null;
        }
    }

    public IEnumerator UseUnitPowerUp(PowerUp powerUp, float[] unitStats)
    {
        _unitInterface.SetCurrentUnitPowerUp(PowerUpType.None);

        float enhancement = powerUp.PowerUpEnhancement;

        switch (powerUp.PowerType)
        {
            case PowerUpType.Adrenaline:
                unitStats[2] += unitStats[2] * enhancement;
                yield return new WaitForSeconds(powerUp.PowerUpDuration);
                unitStats[2] -= unitStats[2] * enhancement;
                break;
            case PowerUpType.Rage:
                unitStats[1] += unitStats[1] * enhancement;
                yield return new WaitForSeconds(powerUp.PowerUpDuration);
                unitStats[1] -= unitStats[1] * enhancement;
                break;
            case PowerUpType.TimeMachine:

                yield return new WaitForSeconds(3);
                break;
            case PowerUpType.None:

                break;
        }


        yield return null;

    }
}
