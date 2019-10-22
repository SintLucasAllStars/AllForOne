using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.UI;
public enum CharacterStates { Idle,Moving}
public class Character : MonoBehaviour
{
    public Actor actor;
    public CharacterStates characterState;
    public ThirdPersonUserControl playerController;
    public ThirdPersonCharacter playerControllerSettings;
    public Animator animator;
    public Rigidbody rigidbody;
    public Collider hand;
    public Slider health;


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
        if (GameManager.instance.gamestate == GameStates.Create)
        {
            animator.enabled = false;
        }
        else
            animator.enabled = true;
        if(transform.position.y < -30)
        {
            Destroy(transform.gameObject);
        }
        switch (characterState)
        {
            case CharacterStates.Idle:
                if (playerControllerSettings.enabled)
                {
                    animator.SetBool("OnGround", true);
                    playerController.enabled = false;
                    playerControllerSettings.enabled = false;
                    animator.SetFloat("Forward", 0);
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.isKinematic = true;
                    hand.enabled = false;
                }
                break;
            case CharacterStates.Moving:
                if (!playerControllerSettings.enabled)
                {
                    playerController.enabled = true;
                    playerControllerSettings.enabled = true;
                    animator.SetFloat("Forward", 0);
                    rigidbody.velocity = Vector3.zero;
                    rigidbody.isKinematic = false;
                    hand.enabled = true;
                    animator.SetBool("OnGround", true);

                }

                if (Input.GetButtonDown("Fire1"))
                {
                    Attack();
                }
                break;
        }
        health.value = actor.health;
    }


    public void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Hit(float damage)
    {
        if(actor.health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (GameManager.instance.gamestate == GameStates.Move)
        {
            animator.SetTrigger("Hit");
            actor.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.gameObject.CompareTag("Player1"))
        {
            if (other.gameObject.CompareTag("Player2"))
            {
                Character p = other.gameObject.GetComponent<Character>();
                p.Hit(actor.strenght);
            }
        }
        if (transform.gameObject.CompareTag("Player2"))
        {
            if (other.gameObject.CompareTag("Player1"))
            {
                Character p = other.gameObject.GetComponent<Character>();
                p.Hit(actor.strenght);
                
            }
        }
    }

    public void Heal(float healAmount)
    {
        actor.Heal(healAmount);
    }
}
