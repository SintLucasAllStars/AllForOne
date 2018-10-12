using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections;
public class Character : MonoBehaviour
{
    private const int maxPoints = 100;
    private const int minPoints = 10;

    [Header("Activation")]
    private ThirdPersonUserControl controller;
    private ThirdPersonCharacter character;

    private float health;

    [SerializeField] private Vector2 damageRange;
    private float strength;

    private float defence;

    [HideInInspector] public bool isFortified;

    private Rigidbody playeRigidbody;

    [Header("Camera")]
    public Transform pivot;
    public Vector3 cameraLocation;

    private Animator animator;

    [SerializeField] private string defaultAnimation;
    [SerializeField] private float defaultAnimationDelay;
    [SerializeField] private float resetDelay;
    [SerializeField] private int defaultDamage;
    [SerializeField] private int defaultRange;

    private Weapon weapon;
    private bool isAttacking = false;
    private PowerUp powerUp;

    private bool isActive = false;


    private void Start()
    {
        controller = GetComponent<ThirdPersonUserControl>();
        character = GetComponent<ThirdPersonCharacter>();
        GameManager.instance.EndRound += EndRound;
        playeRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isActive) return;

        if(Input.GetMouseButtonDown(0))
            Attack();
        if(Input.GetKey(KeyCode.F))
            animator.Play("Death");
        if(Input.GetKey(KeyCode.R))
        {
            animator.Play("Damage");
        }
    }

    public void SetStats(float health, float strength, float speed, float defence)
    {
        this.health = health;
        this.strength = strength;
        controller.runSpeed = speed;
        this.defence = defence;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void EndRound()
    {
        GameManager.instance.EndRound -= EndRound;
        ActivateCharacter(false);
    }


    public void ActivateCharacter(bool activate)
    {
        isActive = activate;
        controller.enabled = activate;
        character.enabled = activate;
        playeRigidbody.isKinematic = !activate;

        if(activate)
        {
            GameManager.instance.EndRound += EndRound;
            gameObject.layer = 2;//Set player to ignoreRaycast
        }
        else
        {
            ResetAnimator();
            gameObject.layer = 10;//Set player layer;
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

        animator.Play((weapon == null) ? defaultAnimation : weapon.animationName);
        isAttacking = true;

        if(weapon != null)
            StartCoroutine(AttackDelay(weapon.animationAttackDelay, weapon.resetDelay));
        else
            StartCoroutine(AttackDelay(defaultAnimationDelay, resetDelay));
    }

    private IEnumerator AttackDelay(float delayTime, float resetDelay)
    {
        int damage = Mathf.RoundToInt(Mathf.Lerp(damageRange.x, damageRange.y, strength)) + ((weapon == null) ? defaultDamage : weapon.damageMultiplier);
        yield return new WaitForSeconds(delayTime);

        Debug.DrawRay(transform.position + transform.up, transform.forward * ((weapon == null) ? defaultRange : weapon.range), Color.red, 10);
        Debug.Log("Excuse");
        RaycastHit hit;
        if(Physics.Raycast(transform.position + transform.up, transform.forward, out hit, ((weapon == null) ? defaultRange : weapon.range)))
        {
            if(hit.collider.CompareTag("Player" + (GameManager.instance.IsTurnPlayerOne ? "Two" : "One")))
            {
                hit.collider.GetComponent<Character>().Damage(damage, Vector3.Angle(hit.collider.transform.forward, transform.forward));
            }
        }
        yield return new WaitForSeconds(resetDelay);
        isAttacking = false;
    }


    public void Damage(int damage, float angle)
    {
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
            Debug.Log("PickUp");
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
