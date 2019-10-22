using System;
using UnityEngine;

namespace MechanicFever
{
    public class UnitPlacementSystem : Singleton<UnitPlacementSystem>
    {
        private bool _hasPlaced = true;

        private void Update()
        {
            if (!_hasPlaced && Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    Tile t;
                    if (t = hit.transform.GetComponent<Tile>())
                    {
                        if (Map.Instance.IsValidNode(t.Position.X, t.Position.Z))
                            PlaceUnit(t.Position);
                    }
                }
            }
        }

        public void PurchaseUnit()
        {
            if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
            {
                Notifier.Instance.ShowNotification("It is not your turn.");
                return;
            }

            if (!Player.Instance.Wallet.CanWithdraw(Player.Instance.PlayerUnit.Price))
            {
                Notifier.Instance.ShowNotification("You are out of money.");
                return;
            }

            _hasPlaced = false;
        }

        private void PlaceUnit(Node node)
        {
            UnitData newUnit = Player.Instance.PlayerUnit;

            newUnit.SetPosition(node);
            newUnit.SetGuid(Guid.NewGuid().ToString());

            GameManager.Instance.SpawnUnit(newUnit);

            Player.Instance.Wallet.Withdraw(newUnit.Price);

            _hasPlaced = true;
        }
    }
}