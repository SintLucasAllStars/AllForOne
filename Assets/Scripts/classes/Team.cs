using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Team {
	public int points = 100;
	public Color teamColor;
	public GameObject teamPrefab;
	public List<Unit> teamUnits;

}
