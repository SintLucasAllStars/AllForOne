using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterController : MonoBehaviour
{
    public bool controllingCurrentCharacter = false;

    private PowerUp activePowerUp;
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

    public int Speed
    {
        get
        {
            return stats.speed;
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

    public void setStats(CharacterStats stats)
    {
        this.stats = stats;
        tag = $"{stats.owner}Owned";
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            addPowerup();
        }

        if (stats != null && controllingCurrentCharacter)
        {
            if (Input.GetKey(KeyCode.Mouse0))
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
        animator.SetFloat("movementSpeed", ((activePowerUp != null && activePowerUp.currentPowerUp == PowerUp.powerUpTypes.adrenaline) ? Speed + (Speed * .5f) : Speed) / 5);
        animator.SetBool("walking", vDirection != 0 || hDirection != 0);
        animator.SetFloat("directionH", hDirection);
        animator.SetFloat("directionV", vDirection);
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");
        float movementSpeed;

        if (activePowerUp != null) { movementSpeed = Speed + (Speed * .5f); }
        else { movementSpeed = Speed; }

        updateMovementAnimation(hDirection, vDirection);
        transform.Translate(hDirection * movementSpeed * Time.deltaTime, 0, vDirection * movementSpeed * Time.deltaTime);
    }
    #endregion

    #region control handelers
    public void startCharacterControl(CharacterSelecter selector)
    {
        controllingCurrentCharacter = true;
        StartCoroutine(controlTimer(selector));
    }

    private IEnumerator controlTimer(CharacterSelecter selector)
    {
        yield return new WaitForSeconds(10);
        ResetCharacter(selector);
    }

    private void ResetCharacter(CharacterSelecter selector)
    {
        StartCoroutine(selector.resetCamera());
        controllingCurrentCharacter = false;
        animator.SetBool("walking", false);

        TurnManager.turnManager.EndTurn();
    }
    #endregion

    #region power ups
    public void addPowerup()
    {
        activePowerUp = TurnManager.turnManager.GetPowerUp();
        TurnManager.turnManager.RemovePowerUp(activePowerUp);
        activePowerUp.StartPowerUp();
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
