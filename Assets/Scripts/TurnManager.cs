using System;
using TMPro;
using UnityEngine;

namespace MechanicFever
{
    public class TurnManager : Singleton<TurnManager>
    {
        private PlayerSide _currentTurn = PlayerSide.Red;
        public PlayerSide CurrentTurn => _currentTurn;

        [SerializeField]
        private TextMeshProUGUI _turnText = null;

        private void ShowTurn() => _turnText.text = "Turn: " + _currentTurn;

        public delegate void OnTurnChanged();
        public static OnTurnChanged ChangeTurn;

        private void OnEnable() => ChangeTurn += ShowTurn;

        private void OnDisable() => ChangeTurn -= ShowTurn;

        private void Start() => ShowTurn();

        public void NextTurn()
        {
            if (_currentTurn != Player.Instance.GameData.PlayerSide)
            {
                Notifier.Instance.ShowNotification("It is not your turn.");
                return;
            }

            //Move on to the next turn. Made dynamic in case there will be more players in the future.
            int i = ((int)_currentTurn + 1) % Enum.GetNames(typeof(PlayerSide)).Length;
            _currentTurn = (PlayerSide)i;

            //Send message to server so that everyone gets the turn change.
            NetworkManager.Instance.SendMessage(new GameData("Turn", Player.Instance.GameData.Guid, Player.Instance.GameData.IsConnected, _currentTurn));
        }

        public bool HasTurn(PlayerSide side) => side == _currentTurn;

        public void SetTurn(PlayerSide playerSide)
        {
            _currentTurn = playerSide;
            ChangeTurn();
        }
    }
}