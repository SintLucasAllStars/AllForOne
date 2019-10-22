using System;
using UnityEngine;
using System.Collections;
public class GameController : Singleton<GameController>
{
    WeaponController weaponController;


    public int width, depth;

    private Player[] players;

    [SerializeField] private GameObject _unitObject;

    public static bool isCurrentPlayerOne;
    private bool isPlacing = false;
    private Unit goingToBePlaced;

    // Use this for initialization
    void Start()
    {
        weaponController = new WeaponController();
        players = new Player[2] {
        new Player(true),
        new Player(false)
    };
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space is clicked");
            UIController.instance.ShowBuyScreen();
        }
        if (isPlacing)
        {
            PlaceUnit(goingToBePlaced);
        }
    }

    public void BuyUnit(Unit _unit, int _strength, int _speed, int _range, int _defense)
    {
        if (isCurrentPlayerOne && players[0].amountOfPoints > 10)
        {
            players[0].units.Add(_unit);
            if (players[1].amountOfPoints - Unit.Cost(_strength, _speed, _range, _defense) > 0)
            {
                Debug.Log("player 1 points:" + players[0].amountOfPoints);
                goingToBePlaced = _unit;
                isPlacing = true;
                UIController.instance.ShowBuyScreen();
            }
            else
            {
                isCurrentPlayerOne = !isCurrentPlayerOne;
                Debug.Log("ran out of monet");
                CameraController.instance.SetView();
            }

        }
        else if (!isCurrentPlayerOne && players[1].amountOfPoints > 10)
        {
            players[1].units.Add(_unit);
            if (players[1].amountOfPoints - Unit.Cost(_strength, _speed, _range, _defense) > 0)
            {
                players[1].amountOfPoints -= Unit.Cost(_strength, _speed, _range, _defense);
                Debug.Log("player 2 points:" + players[1].amountOfPoints);
                goingToBePlaced = _unit;
                isPlacing = true;
                UIController.instance.ShowBuyScreen();
            }
            else
            {
                isCurrentPlayerOne = !isCurrentPlayerOne;
                Debug.Log("ran out of monet");
                GamePhase();
            }
        }


    }

    private void GamePhase()
    {
        UIController.instance.GamePhase();
        if (isCurrentPlayerOne)
        {

        }
        Debug.LogError("playing");
    }

    void PlaceUnit(Unit unit)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            var _placedUnit = Instantiate(_unitObject, hit.point, Quaternion.identity);
            _placedUnit.AddComponent<UnitController>();

            isPlacing = false;
            UIController.instance.ShowBuyScreen();
            isCurrentPlayerOne = !isCurrentPlayerOne;
        }
    }
    #region CountDown

    IEnumerator Timer()
    {
        int timeLeft = 10;
        UIController.instance.CountDownTimer(timeLeft);
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            UIController.instance.CountDownTimer(timeLeft);
        }
        TimersUp();
    }

    private void TimersUp()
    {
        throw new NotImplementedException();
    }

    public void HaltCountDown(int secondsToWait)
    {
        throw new NotImplementedException();
    }
    #endregion
}
