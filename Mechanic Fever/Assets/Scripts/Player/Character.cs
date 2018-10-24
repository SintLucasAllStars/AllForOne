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
    private float fortifyTime;

    private Rigidbody playerRigidbody;
    private CapsuleCollider playerCollider;

    [Header("Camera")]
    public Transform pivot;
    public Vector3 cameraLocation;

    private Animator animator;

    [Header("Weapon Stats")]
    [SerializeField] Weapon defaultWeapon;
    Weapon currentWeapon;
    [SerializeField] private Transform weaponSlot;

    private bool isAttacking = false;

    private List<PowerUp> powerUps = new List<PowerUp>();
    private int currentPowerUp = 0;

    PowerUp activePowerUp;

    [SerializeField] bool isActive = false;


    private void Start()
    {
        controller = GetComponent<ThirdPersonUserControl>();
        character = GetComponent<ThirdPersonCharacter>();
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        defaultWeapon.Init(damage, GetEnemyTag(), weaponSlot);

        UnityEngine.UI.Image image = GetComponentInChildren<UnityEngine.UI.Image>(true);
        GameManager.instance.StartRound += delegate { if(image != null) image.enabled = false; };
        GameManager.instance.EndRound += delegate { if(image != null) image.enabled = true; };
    }

    private void Update()
    {
        if(!isActive || GameManager.instance.gameOver) return;

        if(isFortified && Input.anyKeyDown)
        {
            isFortified = false;
            GameUi.instance.fortified.enabled = false;
        }


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
            SetPowerUpImage();
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
            Destroy(activePowerUp);
            SetPowerUpImage();
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentPowerUp++;
            if(currentPowerUp >= powerUps.Count)
                currentPowerUp = 0;
            SetPowerUpImage();
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            const int requiredFortifyTime = 3;
            fortifyTime = Time.time + requiredFortifyTime;
        }
        else if(Input.GetKeyUp(KeyCode.F))
        {
            isFortified = Time.time > fortifyTime;
            GameUi.instance.fortified.enabled = isFortified;
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

    private void EndRound()
    {
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down, Color.red, 10);
        RaycastHit hit;
        if(Physics.Raycast(transform.position + Vector3.up, Vector3.down, out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("Finish"))
            {
                GameManager.instance.KillCharacter(tag);
                animator.Play("Death");
                Destroy(gameObject, 3);
            }
        }
        else
        {
            GameManager.instance.KillCharacter(tag);
            animator.Play("Death");
            Destroy(gameObject, 3);
        }
        GameManager.instance.EndRound -= EndRound;
        ActivateCharacter(false);
    }

    public void ActivateCharacter(bool activate)
    {
        isActive = activate;
        controller.enabled = activate;
        character.enabled = activate;
        playerRigidbody.constraints = !activate ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.FreezeRotation;

        if(activate)
        {
            GameUi.instance.fortified.enabled = isFortified;
            defaultWeapon.Init(damage, GetEnemyTag(), weaponSlot);
            GameManager.instance.EndRound += EndRound;
            gameObject.layer = 2;//Set player to ignoreRaycast
            playerCollider.radius = .2f;
            SetPowerUpImage();
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
    }

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
        Debug.Log(damage / 2 * defence + " " + damage);
        health -= (isFortified) ? damage - damage / 2 * defence : damage;
        if(health <= 0)
        {
            animator.Play("Death");
            GameManager.instance.KillCharacter();
            gameObject.layer = 0;
            Destroy(gameObject, 5);
            playerRigidbody.isKinematic = true;
            playerCollider.enabled = false;
        }
        else
        {
            animator.SetFloat("Angle", angle);
            animator.Play("Damage");
        }
    }

    private void SetPowerUpImage()
    {
        Debug.Log("Set" + activePowerUp);
        if(activePowerUp == null)
        {
            if(powerUps.Count == 0)
            {
                GameUi.instance.powerUp.enabled = false;
                return;
            }
            Debug.Log("Wrong");
            GameUi.instance.powerUp.sprite = powerUps[currentPowerUp].sprite;
            GameUi.instance.powerUp.rectTransform.localScale = Vector3.one / 2;
        }
        else
        {
            Debug.Log("Here");
            GameUi.instance.powerUp.sprite = activePowerUp.sprite;
            GameUi.instance.powerUp.rectTransform.localScale = Vector3.one;
        }
        GameUi.instance.powerUp.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnActivate();
        }
        else if(other.CompareTag("PickUp"))
        {
            powerUps.Add(other.GetComponent<PowerUp>());
            Destroy(other.GetComponent<SpriteRenderer>());
            SetPowerUpImage();
        }
        else if(other.CompareTag("Weapon"))
        {
            if(currentWeapon != null)
                Destroy(currentWeapon.gameObject);

            currentWeapon = other.GetComponent<Weapon>();
            currentWeapon.Init(damage, GetEnemyTag(), weaponSlot);
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

