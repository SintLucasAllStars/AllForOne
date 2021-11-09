using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterController : MonoBehaviour
{
    public bool controllingCurrentCharacter = false;

    public PowerUp[] activePowerUp;

    public GameObject deathParticle;

    private Animator animator;


    private characterEquipmentHandler equipmentHandler;
    private Weapon equipedWeapon;

    private CharacterStats stats;
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

    public void setStats(CharacterStats stats)
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
        animator = GetComponent<Animator>();
        equipmentHandler = GetComponent<characterEquipmentHandler>();

        activePowerUp = new PowerUp[3];

        equipedWeapon = gameObject.AddComponent<Weapon>();
        equipedWeapon.SetupWeapon(0);

        targetRotation = transform.rotation;
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

            if (Input.GetKey(KeyCode.Mouse1) )
            {
                fortifyTimer += Time.deltaTime;
                if (fortifyTimer >= BaseTimeToFortify)
                {
                    fortified = true;
                    GameManager.gameManager.endTurnEarly();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) && attackResetTimer <= 0)
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
        }

        if (attackResetTimer > 0)
        {
            attackResetTimer -= Time.deltaTime * (equipedWeapon.speed * 0.1f);
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
        GameManager.gameManager.startTimer();
    }

    public void resetCharacter()
    {
        controllingCurrentCharacter = false;
        animator.SetBool("walking", false);
        attackResetTimer = 0;
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

        activePowerUp[activePowerTypeIndex].StartPowerUp();
    }

    public void increasePowerupIndex()
    {
        int count = powerups.Count;
        powerUpIndex++;
        if (powerUpIndex > count)
        {
            powerUpIndex = 0;
        }
    }

    public void decreasePowerupIndex()
    {
        int count = powerups.Count;
        powerUpIndex--;
        if (powerUpIndex < 0)
        {
            powerUpIndex = count;
        }
    }

    public void addPowerUp(PowerUp powerup)
    {
        powerups.Add(powerup);
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
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForSeconds((float)(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 4 - 0.5f));
        attackResetTimer = BaseTimeBetweenAttacks;
        attackHit();
    }

    private void attackHit()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position + transform.forward + (transform.up * 2), new Vector3(2, 2, 2 + equipedWeapon.range));
        foreach (Collider hitObject in hitColliders)
        {
            CharacterController controller;
            if (hitObject.TryGetComponent(out controller) && !hitObject.CompareTag(gameObject.tag))
            {
                controller.TakeDamage(damageCalulation());
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
        die();
        Instantiate(deathParticle, gameObject.transform);
        animator.SetFloat("health", 0);
        animator.SetTrigger("takeDamage");
    }

    public void TakeDamage(float damageValue)
    {
        if (stats.TakeDamage(damageValue /= Defense))
        {
            die();
        }
        animator.SetFloat("health", Health);
        animator.SetTrigger("takeDamage");
    }

    private void die()
    {
        tag = "Untagged";
        Destroy(gameObject, 10);
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + transform.forward + (transform.up * 2), new Vector3(2, 2, 2 + equipedWeapon.range));
    }
}
