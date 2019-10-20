using UnityEngine;
using System;

namespace AllForOne
{
    public class UnitPlacementSystem : Singleton<UnitPlacementSystem>
    {
        private UnitData _currentUnit;
        private bool HasUnit() => _currentUnit != null;

        private int _priceToPay;

        private bool _hasPlaced;

        private void Update()
        {
            if (HasUnit() && !_hasPlaced)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
        }

        public void SetUnit(string b, int price)
        {
            if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
                return;

            _priceToPay = price;
            _hasPlaced = false;
            UnitData playerUnit = Player.Instance.PlayerUnit;
            _currentUnit = new UnitData(Guid.NewGuid().ToString(), new Node(), playerUnit.Type, true, true, Player.Instance.GameData.PlayerSide, playerUnit.Health, playerUnit.Strength, playerUnit.Speed, playerUnit.Defense, playerUnit.Price);
        }

        private void PlaceUnit(Node node)
        {
            _currentUnit.SetPosition(node);
            _currentUnit = null;
            GameManager.Instance.SpawnUnit(_currentUnit);
            _hasPlaced = true;
            Player.Instance.Wallet.Withdraw(_priceToPay);
        }
    }
}