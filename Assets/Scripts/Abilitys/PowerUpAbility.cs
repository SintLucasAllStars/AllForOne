using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAbility :Ability, IPlayerAbilitys
{
    private CharacterController _characterController;

    public override void OnStart()
    {
        _characterController = GetComponent<CharacterController>();
        _characterController.callEveryFrame += EveryFrame;
    }

    public override void EveryFrame()
    {
        if (AbilityPermitted)
		{
			
		}
    }

    public override void BeforeAbility()
    {

    }

    public override void WhileAbility()
    {

    }

    public override void AfterAbility()
    {

    }
}
