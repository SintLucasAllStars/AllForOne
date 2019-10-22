using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace MechanicFever
{
    public enum GameState
    {
        UnitPlacement,
        UnitMovement
    }

    public class TurnManager : Singleton<TurnManager>
    {
        private GameState _gameState = GameState.UnitPlacement;
        public GameState GameState => _gameState;

        public bool CanMoveUnits => _gameState == GameState.UnitMovement;

        private PlayerSide _currentTurn = PlayerSide.Red;
        public PlayerSide CurrentTurn => _currentTurn;

        private Coroutine _timerCoroutine = null;

        private int _turns, _turnTime;
        public int Turns => _turns;

        [SerializeField]
        private TextMeshProUGUI _turnText = null, _turnsText = null, _turnTimeText = null;

        private void ShowTurn()
        {
            _turnText.text = "Turn: " + _currentTurn;
            _turnsText.text = "Turns: " + _turns.ToString();
        }

        public delegate void OnChangeTurn();
        public static OnChangeTurn ChangeTurn;

        public delegate void OnTurnChanged();
        public static OnTurnChanged TurnChanged;

        private void OnEnable() => TurnChanged += ShowTurn;

        private void OnDisable() => TurnChanged -= ShowTurn;

        private void Start() => ShowTurn();

        public void NextTurn()
        {
            if (_currentTurn != Player.Instance.GameData.PlayerSide)
            {
                Notifier.Instance.ShowNotification("It is not your turn.");
                return;
            }

            if(Player.Instance.Wallet.Balance > Player.Instance.PlayerUnit.Price)
            {
                Notifier.Instance.ShowNotification("You need to place more units.");
                return;
            }

            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
                _timerCoroutine = null;
            }

            //Move on to the next turn. Made dynamic in case there will be more players in the future.
            int i = ((int)_currentTurn + 1) % Enum.GetNames(typeof(PlayerSide)).Length;
            _currentTurn = (PlayerSide)i;

            //Send message to server so that everyone gets the turn change.
            GameManager.Instance.ChangeTurn(new GameData("Turn", Player.Instance.GameData.Guid, Player.Instance.GameData.IsConnected, _currentTurn));
            ChangeTurn();
        }

        public bool HasTurn(PlayerSide side) => side == _currentTurn;

        public void SetTurn(PlayerSide playerSide)
        {
            _currentTurn = playerSide;
            _turns++;
            CheckTurn();
            TurnChanged();
        }

        private void CheckTurn()
        {
            if (_turns < 2)
                _gameState = GameState.UnitPlacement;
            if (_turns >= 2)
                _gameState = GameState.UnitMovement;

            //_timerCoroutine = StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            _turnTime = 10;
            while (_turnTime > 0)
            {
                yield return new WaitForSeconds(1);
                _turnTime--;
            }
            NextTurn();
        }

        private void Update()
        {
            _turnTimeText.text = _turnTime.ToString();
        }
    }
}