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
    public Animator animator;
    public Rigidbody rigidbody;

    public void SetActor(Actor actorSettings)
    {
        actor = actorSettings;
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        playerController = GetComponent<ThirdPersonUserControl>();
        playerControllerSettings = GetComponent<ThirdPersonCharacter>();
        characterState = CharacterStates.Idle;
        playerControllerSettings.m_MoveSpeedMultiplier = playerControllerSettings.m_MoveSpeedMultiplier * (actor.speed / 5);
        playerControllerSettings.m_AnimSpeedMultiplier = playerControllerSettings.m_AnimSpeedMultiplier * (actor.speed / 5);

    }
    public void Update()
    {
        switch (characterState)
        {
            case CharacterStates.Idle:
                if (playerControllerSettings.enabled)
                {
                    playerController.enabled = false;
                    playerControllerSettings.enabled = false;
                    animator.SetFloat("Forward", 0);
                    rigidbody.velocity = Vector3.zero;
                }
                break;
            case CharacterStates.Moving:
                if (!playerControllerSettings.enabled)
                {
                    playerController.enabled = true;
                    playerControllerSettings.enabled = true;
                    animator.SetFloat("Forward", 0);
                    rigidbody.velocity = Vector3.zero;
                }
                break;
        }
    }


    public void Attack()
    {

    }
}
