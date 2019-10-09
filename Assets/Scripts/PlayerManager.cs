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

    public Text _countDown;

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
                    EndTurn();
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
        StartCoroutine(EndUnitTurn(unit));
    }

    private IEnumerator EndUnitTurn(PlayableUnit unit)
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
        EndTurn();
        _countDown.gameObject.SetActive(false);
        CurrentGameState = GameStates.CharacterPicking;
    }
}
