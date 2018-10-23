using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour
{
    private const int maxPoints = 100;
    private const int minPoints = 10;

    [Header("Activation")]
    private ThirdPersonUserControl controller;
    private ThirdPersonCharacter character;

    private float health = 100;

    [SerializeField] private Vector2 damageRange;
    private float damage;

    private float defence;

    private float speed;

    public bool isFortified = false;

    private Rigidbody playeRigidbody;
    private CapsuleCollider playerCollider;

    [Header("Camera")]
    public Transform pivot;
    public Vector3 cameraLocation;

    private Animator animator;

    [Header("Weapon Stats")]
    [SerializeField] Weapon defaultWeapon;
    Weapon currentWeapon;

    private bool isAttacking = false;

    private List<PowerUp> powerUps = new List<PowerUp>();
    private int currentPowerUp;

    PowerUp activePowerUp;

    [SerializeField] bool isActive = false;


    private void Start()
    {
        controller = GetComponent<ThirdPersonUserControl>();
        character = GetComponent<ThirdPersonCharacter>();
        playeRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        defaultWeapon.Init(damage, GetEnemyTag());
    }

    private void Update()
    {
        if(!isActive) return;

        if(Input.GetMouseButtonDown(0))
            Attack();

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(powerUps.Count != 0)
            {
                activePowerUp = powerUps[currentPowerUp];
                powerUps.Remove(activePowerUp);
                Debug.Log(activePowerUp);
                activePowerUp.Activate();
                if(activePowerUp.currentType == PowerUp.PowerType.Adernaline)
                {
                    character.m_MoveSpeedMultiplier *= activePowerUp.boost;
                }
                else if(activePowerUp.currentType == PowerUp.PowerType.Rage)
                {
                    defaultWeapon.SetDamage(damage * activePowerUp.boost);
                    if(currentWeapon != null) currentWeapon.SetDamage(damage * activePowerUp.boost);
                }
                else
                {
                    activePowerUp = null;
                }
            }
        }
        else if(activePowerUp != null && activePowerUp.CheckActivity())
        {
            if(activePowerUp.currentType == PowerUp.PowerType.Adernaline)
            {
                character.m_MoveSpeedMultiplier *= speed;
            }
            else if(activePowerUp.currentType == PowerUp.PowerType.Rage)
            {
                defaultWeapon.SetDamage(damage);
                if(currentWeapon != null) currentWeapon.SetDamage(damage);
            }
            activePowerUp = null;
        }
    }

    public void SetStats(float health, float strength, float speed, float defence)
    {
        this.health = health;
        damage = Mathf.Lerp(damageRange.x, damageRange.y, strength);
        character.m_MoveSpeedMultiplier = speed;
        this.speed = speed;
        this.defence = defence;
        playerCollider.enabled = true;
    }

    void EndRound()
    {
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down, Color.red, 10);
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("Finish"))
            {
                GameManager.instance.KillCharacter(tag);
                animator.Play("Death");
                Destroy(gameObject,3);
            }
        }
        GameManager.instance.EndRound -= EndRound;
        ActivateCharacter(false);
    }

    public void ActivateCharacter(bool activate)
    {
        isActive = activate;
        controller.enabled = activate;
        character.enabled = activate;
        playeRigidbody.constraints = !activate ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.FreezeRotation;

        if(activate)
        {
            defaultWeapon.Init(damage, GetEnemyTag());
            GameManager.instance.EndRound += EndRound;
            gameObject.layer = 2;//Set player to ignoreRaycast
            playerCollider.radius = .2f;
        }
        else
        {
            playerCollider.radius = .3f;
            ResetAnimator();
            gameObject.layer = 10;//Set player layer;
            activePowerUp = null;

        }
    }

    private void ResetAnimator()
    {
        animator.SetFloat("Forward", 0);
        animator.SetFloat("Turn", 0);
        animator.SetFloat("Jump", 0);
        animator.SetFloat("JumpLeg", 0);
        animator.SetBool("OnGround", true);
        animator.SetBool("Crouch", false);
    }

    public void Attack()
    {
        if(isAttacking) return;

        Weapon weapon = GetWeapon();
        weapon.Attack();

        animator.Play(weapon.stats.animationName);
        //StartCoroutine(AnimationEnd());
        //isAttacking = true;
    }

    //private IEnumerator AnimationEnd()
    //{
    //    yield return new WaitForSeconds(GetWeapon().stats.animationLength);
    //    isAttacking = false;
    //}

    //private IEnumerator AttackDelay()
    //{
    //    WeaponStats stats = GetWeaponStats();
    //    Debug.Log(stats);
    //    animator.Play(stats.animationName);

    //    yield return new WaitForSeconds(stats.attackDelay);

    //    int damage = Mathf.RoundToInt(Mathf.Lerp(damageRange.x, damageRange.y, strength)) + stats.damageMultiplier;

    //    RaycastHit hit;
    //    if(Physics.Raycast(transform.position + transform.up, transform.forward, out hit, stats.range))
    //        if(hit.collider.CompareTag("Player" + (GameManager.instance.IsTurnPlayerOne ? "Two" : "One")))
    //            hit.collider.GetComponent<Character>().Damage(damage, 0);

    //    yield return new WaitForSeconds(stats.resetDelay);
    //    isAttacking = false;
    //    Debug.Log("ATTACK");
    //}

    public Weapon GetWeapon()
    {
        return currentWeapon == null ? defaultWeapon : currentWeapon;
    }

    public string GetEnemyTag()
    {
        return "Player" + (GameManager.instance.IsTurnPlayerOne ? "Two" : "One");
    }

    public void Damage(int damage, float angle)
    {
        Debug.Log((isFortified) ? damage - damage / 2 * (1 - defence) : damage);
        health -= (isFortified) ? damage - damage / 2 * (1 - defence) : damage;
        if(health <= 0)
        {
            animator.Play("Death");
            GameManager.instance.KillCharacter();
            gameObject.layer = 0;
            Destroy(gameObject, 5);
        }
        else
        {
            animator.SetFloat("Angle", angle);
            animator.Play("Damage");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnActivate();
        }
        else if(other.CompareTag("PickUp"))
        {
            Debug.Log(other.GetComponent<PowerUp>());
            powerUps.Add(other.GetComponent<PowerUp>());
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Weapon"))
        {
            if(currentWeapon != null)
                Destroy(currentWeapon.gameObject);

            currentWeapon = other.GetComponent<Weapon>();
            currentWeapon.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnDeactivate();
        }
    }
}

