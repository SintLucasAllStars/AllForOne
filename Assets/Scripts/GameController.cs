using System;
using UnityEngine;
using System.Collections;
public class GameController : Singleton<GameController>
{
    WeaponController weaponController;


    public int width, depth;

    public Player[] players;

    [SerializeField] private GameObject _unitObject;

    public static bool isCurrentPlayerOne;
    private bool isPlacing = false;
    private Unit goingToBePlaced;
    private LayerMask layerToPlace;

    private bool isGamePhase = false;

    private GameObject currentObj;

    // Use this for initialization
    void Start()
    {
        UIController.instance.ShowBuyScreen();
        weaponController = new WeaponController();
        players = new Player[2] {
        new Player(true),
        new Player(false)
    };
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            PlaceUnit(goingToBePlaced, isCurrentPlayerOne);
        }
        if (isGamePhase)
        {
            GamePhase();
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
                Debug.Log("ran out of money");
                isGamePhase = true;
                UIController.instance.GamePhase();
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
                Debug.Log("ran out of money");
                isGamePhase = true;
                UIController.instance.GamePhase();
            }
        }


    }

    private void GamePhase()
    {
        TakeControl();
    }

    private void TakeControl()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit) && Input.GetMouseButtonDown(0))
        {
            Debug.Log("Fired ray");
            if (hit.transform.gameObject.CompareTag("Unit"))
            {
                Debug.Log("taking Control");
                hit.transform.GetComponent<UnitController>().isControlled = true;
                CameraController.instance.SetTarget(hit.transform);
                currentObj = hit.transform.gameObject;
                StartCoroutine(Timer());
            }
        }
    }

    void PlaceUnit(Unit unit, bool isOne)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        int layermask = 1 << 9;
        layerToPlace = layermask;

        if (Physics.Raycast(ray, out hit, layerToPlace) && Input.GetMouseButtonDown(0))
        {
            var _placedUnit = Instantiate(_unitObject, hit.point, hit.transform.rotation);
            _placedUnit.AddComponent<UnitController>().SetColor(isOne);
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
        var control = currentObj.GetComponent<UnitController>();
        control.isControlled = false;
        control.CheckIfInside();
        CameraController.isFollowing = false;
        isCurrentPlayerOne = !isCurrentPlayerOne;
        UIController.instance.SetCurrentPlayerText();
    }

    public void HaltCountDown(int secondsToWait)
    {
        Debug.LogError("the powerups are not in working condition");
        throw new NotImplementedException();
    }
    #endregion
}
