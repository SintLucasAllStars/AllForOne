using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class UnitController : MonoBehaviour
{
    public bool controllingCurrentCharacter = false;

    public PowerUp[] activePowerUp;

    public GameObject deathParticle;

    private Animator animator;


    private UnitCharacterEquimentManager equipmentHandler;
    private Weapon equipedWeapon;

    private UnitStats stats;
    #region stats property fields
    public float Health
    {
        get
        {
            return stats.health;
        }
    }

    public float Strength
    {
        get
        {
            float value = stats.strength;
            if (activePowerUp[0] != null) { value *= 1.1f; }
            return value;
        }
    }

    public float Speed
    {
        get
        {
            float value = stats.speed;
            if (activePowerUp[0] != null) { value *= 1.5f; }
            return value;
        }
    }

    public float Defense
    {
        get
        {
            return stats.getDefense();
        }
    }

    public bool fortified
    {
        set
        {
            animator.SetBool("blocking", value);
            stats.SetFortified(value);
        }

        get
        {
            return stats.fortified;
        }
    }
    #endregion

    public List<PowerUp> powerups = new List<PowerUp>();
    private int powerUpIndex = 0;

    private Quaternion targetRotation;

    private float attackResetTimer;
    public float BaseTimeBetweenAttacks;
    public float BaseTimeToFortify;
    private float fortifyTimer = 0;

    public void setStats(UnitStats stats)
    {
        this.stats = stats;
        tag = $"{stats.owner}Owned";
        if (insideCheck())
        {
            instantDeath();
        }
    }

    private void Start()
    {
        Init();
        GameManager.gameManager.OnReset.AddListener(Init);
    }

    private void Init()
    {
        animator = GetComponent<Animator>();
        equipmentHandler = GetComponent<UnitCharacterEquimentManager>();

        activePowerUp = new PowerUp[3];

        equipedWeapon = gameObject.AddComponent<Punch>();
        equipedWeapon.WeaponSetup();

        targetRotation = transform.rotation;

        attackResetTimer = BaseTimeBetweenAttacks;
    }

    // Update is called once per frame
    void Update()
    {
        if (stats != null && controllingCurrentCharacter)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && powerups.Count > 0 && powerups[powerUpIndex] != null)
            {
                activatePowerup();
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                increasePowerupIndex();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                decreasePowerupIndex();
            }

            if (Input.GetKey(KeyCode.Mouse1))
            {
                animator.SetBool("walking", false);

                fortifyTimer += Time.deltaTime;
                UiManager.uiManager.updateFortified(fortifyTimer, BaseTimeToFortify);
                if (fortifyTimer >= BaseTimeToFortify)
                {
                    fortified = true;
                    GameManager.gameManager.endTurnEarly();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                fortifyTimer = 0;
                UiManager.uiManager.updateFortified(fortifyTimer, BaseTimeToFortify);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackResetTimer >= BaseTimeBetweenAttacks)
                {
                    updateAttackAnimation();
                    StartCoroutine(attackTimer());
                }
                else
                {
                    Movement();
                    rotateCamera();
                }
            }

            if (attackResetTimer <= BaseTimeBetweenAttacks)
            {
                attackResetTimer += Time.deltaTime * (equipedWeapon.speed * 0.1f);
                UiManager.uiManager.updateAttackTimer(attackResetTimer, BaseTimeBetweenAttacks);
            }
        }
    }

    #region movement

    private void updateMovementAnimation(float hDirection, float vDirection)
    {
        animator.SetFloat("movementSpeed", (float)Speed / 5);
        animator.SetBool("walking", vDirection != 0 || hDirection != 0);
        animator.SetFloat("directionH", hDirection);
        animator.SetFloat("directionV", vDirection);
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");

        updateMovementAnimation(hDirection, vDirection);
        transform.Translate(hDirection * Speed * Time.deltaTime, 0, vDirection * Speed * Time.deltaTime);
    }

    private void rotateCamera()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            targetRotation *= Quaternion.Euler(0, 45, 0);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation *= Quaternion.Euler(0, -45, 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 180 * Time.deltaTime);
    }
    #endregion

    #region control handelers
    public void startCharacterControl()
    {
        controllingCurrentCharacter = true;
        fortified = false;

        UiManager.uiManager.showUnitActionUi();
        UiManager.uiManager.updateAttackTimer(BaseTimeBetweenAttacks, BaseTimeBetweenAttacks);
        UiManager.uiManager.updateFortified(fortifyTimer, BaseTimeToFortify);

        if (powerups.Count > 0)
        {
            UiManager.uiManager.cycleThroughPowerups(powerups[0].powerUpType);
            UiManager.uiManager.showPowerUpUi();
        }

        GameManager.gameManager.startTimer();
    }

    public void resetCharacter()
    {
        UiManager.uiManager.disableUnitActionUi();
        UiManager.uiManager.disablePowerUpUi();

        controllingCurrentCharacter = false;
        animator.SetBool("walking", false);
        attackResetTimer = BaseTimeBetweenAttacks;
        fortifyTimer = 0;
        if (insideCheck())
        {
            instantDeath();
        }
    }
    #endregion

    #region power ups
    public void activatePowerup()
    {
        int activePowerTypeIndex = powerups[powerUpIndex].powerUpType;

        if (activePowerUp[2] != null && activePowerTypeIndex == 2) { activePowerUp[2].CancelPowerUp(); }

        activePowerUp[activePowerTypeIndex] = powerups[powerUpIndex];

        removePowerUp(powerUpIndex);

        activePowerUp[activePowerTypeIndex].ActivatePowerUp();

        powerUpIndex = 0;

        if (powerups.Count <= 0)
        {
            UiManager.uiManager.cycleThroughPowerups(-1);
        }
        else
        {
            UiManager.uiManager.cycleThroughPowerups(powerups[powerUpIndex].powerUpType);
        }
    }

    public void increasePowerupIndex()
    {
        int count = powerups.Count;
        powerUpIndex++;
        if (powerUpIndex >= count)
        {
            powerUpIndex = 0;
        }

        UiManager.uiManager.cycleThroughPowerups(powerups[powerUpIndex].powerUpType);
    }

    public void decreasePowerupIndex()
    {
        int count = powerups.Count;
        powerUpIndex--;
        if (powerUpIndex < 0)
        {
            powerUpIndex = count - 1;
        }

        UiManager.uiManager.cycleThroughPowerups(powerups[powerUpIndex].powerUpType);
    }

    public void addPowerUp(PowerUp powerup)
    {
        powerups.Add(powerup);

        UiManager.uiManager.cycleThroughPowerups(powerups[powerUpIndex].powerUpType);
        UiManager.uiManager.showPowerUpUi();
    }

    public void removePowerUp(int index)
    {
        powerups.RemoveAt(index);
    }
    #endregion

    #region weapons and equipment
    public void addWeapon(Weapon weapon)
    {
        if (equipedWeapon != null)
        {
            if (equipedWeapon.index != 0)
            {
                equipmentHandler.unEquipWeapon(equipedWeapon.index);
            }
            equipedWeapon.destroyWeapon();
        }
        equipedWeapon = weapon;
        animator.SetInteger("attackIndex", equipedWeapon.index);
        equipmentHandler.EquipWeapon(equipedWeapon.index);
    }
    #endregion

    #region attacks and damage
    public void updateAttackAnimation()
    {
        animator.SetFloat("attackSpeed", equipedWeapon.speed * 0.1f);
        animator.SetTrigger("Attack");
    }

    public IEnumerator attackTimer()
    {
        attackResetTimer = 0;
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds((float)(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 4 - 0.5f));
        attackHit();
    }

    private void attackHit()
    {
        RaycastHit rayhit;
        Vector3 weaponOffset = transform.position + (Vector3.up * 2);
        Collider[] hitColliders = Physics.OverlapBox(weaponOffset + (transform.forward * ((equipedWeapon.range / 2) + 2)),
            new Vector3(1.5f, 1.5f, 2) + (transform.forward * equipedWeapon.range));
        foreach (Collider hitObject in hitColliders)
        {
            UnitController controller;
            if (!hitObject.CompareTag(gameObject.tag))
            {
                if (Physics.Raycast(weaponOffset, transform.TransformDirection(Vector3.forward), out rayhit))
                {
                    if (rayhit.collider.TryGetComponent(out controller))
                    {
                        controller.TakeDamage(damageCalulation());
                    }
                }
            }
        }
    }

    private float damageCalulation()
    {
        return equipedWeapon.damage * Strength;
    }
    #endregion

    #region death and hit mechanic's
    public bool insideCheck()
    {
        Vector3 hightOffset = new Vector3(0, 0.5f, 0);
        RaycastHit hit;
        Physics.Raycast(transform.position + hightOffset, -transform.up, out hit);
        return !hit.collider.CompareTag("insideFlooring");
    }

    public void instantDeath()
    {
        if (tag != "Untagged")
        {
            die();
            Instantiate(deathParticle, gameObject.transform);
            animator.SetFloat("health", 0);
            animator.SetInteger("RandomDeath", Random.Range(0, 2));
            animator.SetTrigger("takeDamage");
        }
    }

    public void TakeDamage(float damageValue)
    {
        if (stats.TakeDamage(damageValue /= Defense))
        {
            die();
        }
        animator.SetFloat("health", Health);
        animator.SetInteger("RandomDeath", Random.Range(0, 2));
        animator.SetTrigger("takeDamage");
    }

    private void die()
    {
        tag = "Untagged";
        GameManager.gameManager.unitDeath(gameObject, stats.ownerNumber);
        Destroy(gameObject, 10);
    }
    #endregion
}
