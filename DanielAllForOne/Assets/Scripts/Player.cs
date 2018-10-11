using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player {

    private Color _teamColor;
    private List<Unit> _playerUnitList = new List<Unit>(0);

    public Player(Color teamColor)
    {
        _teamColor = teamColor;
    }
	
}
