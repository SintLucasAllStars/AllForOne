using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public GameObject _characterCreator;

    public GameStates _currentGameState = GameStates.Hiring;

    public List<Player> _players = new List<Player>();

    public Player _activePlayer;

    public Camera _topDownCamera;

    public Animator _playerKiller;

    public Text _countDown;

    public GameObject _victoryUI;

    private Coroutine _waitCoroutine = null;

    public GameStates CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            _currentGameState = value;

            switch (_currentGameState)
            {
                case GameStates.Hiring:
                    break;
                case GameStates.CharacterPicking:
                    _characterCreator.SetActive(false);
                    CheckOutsideUnits();
                    break;
                case GameStates.Movement:
                    break;
                default:
                    break;
            }
        }
    }

    private void Awake()
    {
        Instance = this;
        _activePlayer = _players[0];
    }

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameStates.Hiring:
                break;
            case GameStates.CharacterPicking:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        PlayableUnit unit = hit.collider.gameObject.GetComponent<PlayableUnit>();
                        if (unit != null && unit._player == _activePlayer)
                        {
                            ActivateUnit(unit);
                        }
                    }
                }
                break;
            case GameStates.Movement:
                break;
            default:
                break;
        }
    }

    public void EndTurn()
    {
        _activePlayer = GetOtherPlayer();

        switch (CurrentGameState)
        {
            case GameStates.Hiring:
                if (_activePlayer._currency <= 10)
                {
                    _activePlayer = GetOtherPlayer();
                }
                break;
            case GameStates.CharacterPicking:
                break;
            case GameStates.Movement:
                break;
            default:
                break;
        }
    }

    public void CheckMoneyForRemainingMoney()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i]._currency >= 10)
            {
                return;
            }
        }

        CurrentGameState = GameStates.CharacterPicking;
    }

    public void Suicide(PlayableUnit unit)
    {
        EndUnitTurn(unit);
    }

    public void CheckVictory()
    {
        List<PlayableUnit> playableUnits = new List<PlayableUnit>();
        playableUnits.AddRange(FindObjectsOfType<PlayableUnit>());

        int p1Units = 0;
        int p2Units = 0;

        foreach (PlayableUnit unit in playableUnits)
        {
            if (unit._player._playerID == 1 && unit._isAlive)
            {
                p1Units++;
            }
            else if (unit._isAlive)
            {
                p2Units++;
            }
        }

        if (p1Units == 0)
        {
            Victory(_players[1]._playerID);
        }
        else if (p2Units == 0)
        {
            Victory(_players[0]._playerID);
        }
    }

    private Player GetOtherPlayer()
    {
        foreach (Player player in _players)
        {
            if (player != _activePlayer)
            {
                return player;
            }
        }
        return null;
    }

    private void ActivateUnit(PlayableUnit unit)
    {
        unit.IsActive = true;
        _waitCoroutine = StartCoroutine(WaitUnitTurn(unit));
    }

    public void EndUnitTurn(PlayableUnit unit)
    {
        StopCoroutine(_waitCoroutine);
        EndTurn();
        _countDown.gameObject.SetActive(false);
        CurrentGameState = GameStates.CharacterPicking;
    }

    private IEnumerator WaitUnitTurn(PlayableUnit unit)
    {
        CurrentGameState = GameStates.Movement;
        int timeRemaining = 10;
        _countDown.gameObject.SetActive(true);
        _countDown.text = timeRemaining.ToString();

        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            _countDown.text = timeRemaining.ToString();
        }

        unit.IsActive = false;
        EndUnitTurn(unit);
    }

    private void Victory(int playerID)
    {
        _victoryUI.SetActive(true);
        _victoryUI.GetComponentInChildren<Text>().text = "Victory for player " + playerID.ToString();
    }

    private void CheckOutsideUnits()
    {
        _playerKiller.Play("SunBurn");

        List<PlayableUnit> allUnits = new List<PlayableUnit>();
        allUnits.AddRange(FindObjectsOfType<PlayableUnit>());

        foreach (PlayableUnit unit in allUnits)
        {
            unit.CheckIndoor();
        }
    }
}
