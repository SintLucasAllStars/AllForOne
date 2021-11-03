using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterController : MonoBehaviour
{
    public bool controllingCurrentCharacter = false;

    public PowerUp[] activePowerUp;
    private Animator animator;
    private CharacterStats stats;
    #region stats property fields
    public int Health
    {
        get
        {
            return stats.health;
        }
    }

    public int Strength
    {
        get
        {
            return stats.strength;
        }
    }

    public float Speed
    {
        get
        {
            float value = stats.speed;
            if(activePowerUp[0] != null) {value *= 1.5f;}
            return value;
        }
    }

    public int Defense
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
    }

    private void Start()
    {
        animator = GetComponent<Animator>();

        activePowerUp = new PowerUp[3];
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
        TurnManager.turnManager.startTimer();
    }

    public void resetCharacter()
    {
        controllingCurrentCharacter = false;
        animator.SetBool("walking", false);
    }
    #endregion

    #region power ups
    public void activatePowerup()
    {
        int activePowerTypeIndex = powerups[powerUpIndex].powerUpType;
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

    #region death and hit mechanic's
    public void TakeDamage(int damageValue)
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
