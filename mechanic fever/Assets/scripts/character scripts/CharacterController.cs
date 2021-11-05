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
    #endregion

    public List<PowerUp> powerups = new List<PowerUp>();
    private int powerUpIndex = 0;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            instantDeath();
        }

        if (stats != null && controllingCurrentCharacter)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && powerups.Count > 0 && powerups[powerUpIndex] != null)
            {
                activatePowerup();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                animator.SetTrigger("Attack");
            }
            else
            {
                Movement();
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
    #endregion

    #region control handelers
    public void startCharacterControl()
    {
        controllingCurrentCharacter = true;
        GameManager.gameManager.startTimer();
    }

    public void resetCharacter()
    {
        controllingCurrentCharacter = false;
        animator.SetBool("walking", false);
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
        if (equipedWeapon != null) { equipedWeapon.destroyWeapon(); }
        equipedWeapon = weapon;
        animator.SetInteger("attackIndex", equipedWeapon.index);
        equipmentHandler.EquipWeapon(equipedWeapon.index);
    }
    #endregion

    #region death and hit mechanic's
    public bool insideCheck()
    {
        Vector3 hightOffset = new Vector3(0, 0.5f, 0);
        RaycastHit hit;
        Physics.Raycast(transform.position + hightOffset, -transform.up, out hit);
        Debug.DrawRay(transform.position + hightOffset, -transform.up, Color.red, 20);
        return !hit.collider.CompareTag("insideFlooring");
    }

    public void instantDeath()
    {
        Destroy(gameObject, 15);
        Instantiate(deathParticle, gameObject.transform);
        animator.SetFloat("health", 0);
        animator.SetTrigger("takeDamage");
    }

    public void TakeDamage(float damageValue)
    {
        if (stats.TakeDamage(damageValue -= Defense))
        {
            Destroy(gameObject, 15);
        }
        animator.SetFloat("health", Health);
        animator.SetTrigger("takeDamage");
    }
    #endregion
}
