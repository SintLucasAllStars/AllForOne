using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledAttackAbility : Ability, IPlayerAbilitys
{
	public Weapon startWeapon;
	public GameObject[] weaponsOnPlayer;
	//[HideInInspector]
	public Weapon currentWeapon;

	Weapon oldWeapon;

	CharacterController _characterController;
	float attackRange = 1;
	public float damage = 10;
	public float strength;
	[SerializeField]
	float coolDown = 1;
	[SerializeField]
	LayerMask thingsToAttack;
	[SerializeField]
	float attackLentgh = 1;
	[SerializeField]
	GameObject hitParticle;
	[SerializeField]
	GameObject killParticle;

	RaycastHit hit;
	bool canAttack = true; // check if cooldown is done;

	public override void OnStart()
	{
		_characterController = GetComponent<CharacterController>();
		_characterController.callEveryFrame += EveryFrame;
		currentWeapon = startWeapon;
		coolDown = 11 - currentWeapon.cooldown;
        damage = currentWeapon.damage;
        attackRange = 2 + currentWeapon.range;
		for (int i = 0; i < weaponsOnPlayer.Length; i++)
        {
            if (i + 1 == currentWeapon.weaponID)
            {
                weaponsOnPlayer[i].SetActive(true);
            }
            else
            {
                weaponsOnPlayer[i].SetActive(false);
            }
        }
	}

	public override void EveryFrame()
	{
		if (AbilityPermitted)
		{
			if(currentWeapon != oldWeapon){
				oldWeapon = currentWeapon;
				if(currentWeapon.weaponID > 0){
					for (int i = 0; i < weaponsOnPlayer.Length; i++){
						if(i+1 == currentWeapon.weaponID){
							weaponsOnPlayer[i].SetActive(true);
						}else{
							weaponsOnPlayer[i].SetActive(false);
						}                  
					}
					coolDown = 11-currentWeapon.cooldown;
					damage = currentWeapon.damage;
					attackRange = 2+currentWeapon.range;
				}
			}
			Debug.DrawRay(transform.position+(Vector3.up*2), transform.forward , Color.red, attackRange, false);
			Debug.DrawRay(transform.position + (Vector3.up * 2) + (transform.right/2), transform.forward, Color.blue, attackRange, false);
			Debug.DrawRay(transform.position + (Vector3.up * 2) + (-transform.right/2), transform.forward, Color.green, attackRange, false);
			if (InputManager.Instance.attackButton)
			{
				BeforeAbility();
			}
		}
	}
	IEnumerator Timer(){
		yield return new WaitForSeconds(coolDown);
		canAttack = true;
	}
	public override void BeforeAbility()
	{
		if (canAttack)
		{
			canAttack = false;
			Ray ray = new Ray(transform.position + (Vector3.up * 2), transform.forward);
			Ray ray1 = new Ray(transform.position + (Vector3.up * 2)+transform.right/2, transform.forward);
			Ray ray2 = new Ray(transform.position + (Vector3.up * 2)+ (-transform.right/2), transform.forward);
			if (!_characterController.stateLocked)
			{
				_characterController.currentPlayerState = CharacterController.PlayerStates.attacking;
				_characterController.stateLocked = true;
			}
			if (Physics.Raycast(ray, out hit, attackRange, thingsToAttack)||Physics.Raycast(ray1, out hit, attackRange, thingsToAttack)||Physics.Raycast(ray2, out hit, attackRange, thingsToAttack))
			{
				Invoke("WhileAbility", 0.5f);
			}
			else
			{
				Invoke("AfterAbility", attackLentgh);
			}
			StartCoroutine(Timer());
		}
	}

	public override void WhileAbility()
	{
		if(hit.transform == null){
            Invoke("AfterAbility", attackLentgh);
			return;
		}
		CharacterController objectToHit = hit.transform.GetComponent<CharacterController>();
		if (objectToHit != null)
		{
			if (objectToHit.team != _characterController.team)
			{
				FortifieAbility fortifieAbility = objectToHit.GetComponent<FortifieAbility>();
				float finalDamage = damage * (strength / 10);
				if(fortifieAbility.fortified){
					finalDamage = (damage * (strength / 10)) - fortifieAbility.defense;
				}
				objectToHit.health -= finalDamage;
				if (objectToHit.health <= 0)
				{
					objectToHit.dead = true;
					if (killParticle != null)
					{
						Instantiate(killParticle, hit.transform.position, Quaternion.identity);
					}
				}
				else
				{
					if (hitParticle != null)
					{
						Instantiate(hitParticle, hit.transform.position+(Vector3.up*2), Quaternion.identity);
					}
				}
			}
		}
        Invoke("AfterAbility", attackLentgh);


	}

	public override void AfterAbility()
	{
		_characterController.stateLocked = false;
		_characterController.currentPlayerState = CharacterController.PlayerStates.idle;
	}
}
