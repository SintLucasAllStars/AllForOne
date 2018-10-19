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

    public IEnumerator PlayerMovement(Transform unitTransform)
    {
        Unit unit = unitTransform.gameObject.GetComponent<Unit>();

        Camera.main.transform.SetParent(unitTransform);

        Vector3 target = new Vector3(0, unitTransform.localPosition.y, -2);

        while (Vector3.Distance(Camera.main.transform.localPosition, target) >= 0.01f)
        {
            Camera.main.transform.localPosition = Vector3.Lerp(Camera.main.transform.localPosition, target, Time.deltaTime * 3);
            Camera.main.transform.LookAt(unitTransform);
            yield return null;
        }

        unit.IsUnitActive = true;

        float time = 10;

        while (time >= 0)
        {
            Vector3 offset = Camera.main.transform.position - unitTransform.position;

            Camera.main.transform.position = offset + unitTransform.position;

            float translation = Input.GetAxis("Vertical") * unit.UnitStats[2];
            float rotation = Input.GetAxis("Horizontal") * unit.UnitStats[2];

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            unitTransform.Translate(0, 0, translation);

            unitTransform.Rotate(0, rotation, 0);
            time -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Q) && unit.UnitPowerUp != null)
                StartCoroutine(UseUnitPowerUp(unit.UnitPowerUp, unit.UnitStats));

            yield return null;
        }

        unit.IsUnitActive = false;

        SetNextPlayerIndex();

        StartCoroutine(_unitSelectionManager.UnitSelection());
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
