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

        private void Start() => ShowTurn();

        public void NextTurn()
        {
            if (_currentTurn != Player.Instance.GameData.PlayerSide)
                return;

            //Move on to the next turn. Made dynamic in case there will be more players in the future.
            int i = ((int)_currentTurn + 1) % Enum.GetNames(typeof(PlayerSide)).Length;
            _currentTurn = (PlayerSide)i;

            NetworkManager.Instance.SendMessage(new GameData("Turn", Player.Instance.GameData.Guid, Player.Instance.GameData.IsConnected, _currentTurn));
        }

        private void ShowTurn() => _turnText.text = "Turn: " + _currentTurn;

        public bool HasTurn(PlayerSide side) => side == _currentTurn;

        public void SetTurn(PlayerSide playerSide)
        {
            _currentTurn = playerSide;
            ShowTurn();
        }
    }
}