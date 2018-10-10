using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueChanger : MonoBehaviour {
	WalkAbility walkAbility;
	CharacterController characterController;
	ControlledAttackAbility attackAbility;
	FortifieAbility fortifieAbility;

	public void UpdateValues(float speed,float health,float defense,float strength,int team){
		if(walkAbility == null){
			walkAbility = GetComponent<WalkAbility>();
            characterController = GetComponent<CharacterController>();
            attackAbility = GetComponent<ControlledAttackAbility>();
			fortifieAbility = GetComponent<FortifieAbility>();
		}
		characterController.team = team;
		attackAbility.strength = strength;
		walkAbility.speed = speed / 3;
		walkAbility.runSpeed = speed;
		characterController.health = health;
		fortifieAbility.defense = defense;
	}
}
