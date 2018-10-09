using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortifieAbility : Ability, IPlayerAbilitys {

	private CharacterController _characterController;

	[SerializeField]
	float fortifieTime = 3;


	[HideInInspector]
	public bool fortified = false;

	[HideInInspector]
	public float defense = 0;

    float timer = 0;

	public override void OnStart()
    {
		_characterController = GetComponent<CharacterController>();
        _characterController.callEveryFrame += EveryFrame;
    }

    public override void EveryFrame()
    {
        if (AbilityPermitted)
        {
			if (InputManager.Instance.fortifieButton)
			{
				BeforeAbility();
			}else{
				timer = 0;
			}
			if (_characterController.currentPlayerState != CharacterController.PlayerStates.fortifying && _characterController.currentPlayerState != CharacterController.PlayerStates.fortified)
            {
				fortified = false;
            }
        }
    }

    public override void BeforeAbility()
    {
		timer += Time.deltaTime;
		_characterController.currentPlayerState = CharacterController.PlayerStates.fortifying;
		if(timer > fortifieTime){
			WhileAbility();
		}
    }

    public override void WhileAbility()
	{
        _characterController.currentPlayerState = CharacterController.PlayerStates.fortified;
		fortified = true;
	
	}
}
