using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlledAttackAbility : Ability, IPlayerAbilitys
{

	CharacterController _characterController;
	[SerializeField]
	float attackRange = 5;
	public float damage = 10;
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
	}

	public override void EveryFrame()
	{
		if (AbilityPermitted)
		{
			Debug.DrawRay(transform.position+(Vector3.up*2), transform.forward , Color.red, attackRange, false);
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
			if (!_characterController.stateLocked)
			{
				_characterController.currentPlayerState = CharacterController.PlayerStates.attacking;
				_characterController.stateLocked = true;
			}
			if (Physics.Raycast(ray, out hit, attackRange, thingsToAttack))
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
				float finalDamage = damage;
				if(fortifieAbility.fortified){
					finalDamage = damage - fortifieAbility.defense;
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
