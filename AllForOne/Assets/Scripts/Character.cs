using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
public enum CharacterStates { Idle,Moving}
public class Character : MonoBehaviour
{
    public Actor actor;
    public CharacterStates characterState;
    public ThirdPersonUserControl playerController;
    public ThirdPersonCharacter playerControllerSettings;

    public void SetActor(Actor actorSettings)
    {
        actor = actorSettings;
        playerController = GetComponent<ThirdPersonUserControl>();
        playerControllerSettings = GetComponent<ThirdPersonCharacter>();
        characterState = CharacterStates.Idle;
        playerControllerSettings.m_MoveSpeedMultiplier = playerControllerSettings.m_MoveSpeedMultiplier * actor.speed / 4;
        playerControllerSettings.m_AnimSpeedMultiplier = playerControllerSettings.m_AnimSpeedMultiplier * actor.speed / 4;

    }
    public void Update()
    {
        switch (characterState)
        {
            case CharacterStates.Idle:
                if(playerController.enabled)
                playerController.enabled = false;
                break;
            case CharacterStates.Moving:
                if (!playerController.enabled)
                    playerController.enabled = true;
                break;
        }
    }


    public void Attack()
    {

    }
}
