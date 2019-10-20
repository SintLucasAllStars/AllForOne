using System;
using UnityEngine;

namespace AllForOne
{
    public class TurnManager : Singleton<TurnManager>
    {
        private PlayerSide _currentTurn;
        public PlayerSide CurrentTurn => _currentTurn;

        public void NextTurn()
        {
            if (_currentTurn != Player.Instance.GameData.PlayerSide)
                return;

            //Move on to the next turn. Made dynamic in case there will be more players in the future.
            int i = ((int)_currentTurn + 1) % Enum.GetNames(typeof(PlayerSide)).Length;
            _currentTurn = (PlayerSide)i;

            NetworkManager.Instance.SendMessage(new GameData("Turn", Player.Instance.GameData.Guid, Player.Instance.GameData.IsConnected, _currentTurn));
        }

        public bool HasTurn(PlayerSide side) => side == _currentTurn;

        public void SetTurn(PlayerSide playerSide) => _currentTurn = playerSide;
    }
}