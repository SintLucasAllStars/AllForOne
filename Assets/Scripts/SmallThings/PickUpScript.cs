using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour {
	public Weapon weapon;
	public PowerUp powerUp;

	private void OnTriggerEnter(Collider collision)
	{
		if(weapon !=null){
			collision.transform.GetComponent<ControlledAttackAbility>().currentWeapon = weapon;
		}
		if(powerUp !=null){
			
		}
		Destroy(this.gameObject);
	}
}
