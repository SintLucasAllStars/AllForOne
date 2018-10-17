using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private List<Unit> _playerUnitList = new List<Unit>(0);

    public Player(Color teamColor)
    {
        GetTeamColor = teamColor;
    }

    public Color GetTeamColor { get; private set; }

}
