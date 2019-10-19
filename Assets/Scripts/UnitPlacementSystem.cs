﻿using UnityEngine;
using System;

namespace AllForOne
{
    public class UnitPlacementSystem : Singleton<UnitPlacementSystem>
    {
        private UnitData _currentUnit;
        private bool HasUnit() => _currentUnit != null;

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

        public void SetUnit(string b)
        {
            if (!TurnManager.Instance.HasTurn(Player.Instance.GameData.PlayerSide))
                return;

            _hasPlaced = false;
            _currentUnit = new UnitData(Guid.NewGuid().ToString(), new Node(), b, true, true, Player.Instance.GameData.PlayerSide, 0);
        }

        private void PlaceUnit(Node node)
        {
            _currentUnit.SetPosition(node);
            GameManager.Instance.SpawnUnit(_currentUnit);
            _hasPlaced = true;

            TurnManager.Instance.NextTurn();
        }
    }
}