using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterController : MonoBehaviour
{
    public bool controllingCurrentCharacter = false;

    private Animator animator;
    private CharacterStats stats;
    #region stats property fields
    private int health
    {
        get
        {
            return stats.health;
        }
    }

    private int strength
    {
        get
        {
            return stats.strength;
        }
    }

    private int speed
    {
        get
        {
            return stats.speed;
        }
    }

    private int defense
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
        print(this.stats.ToString());
        tag = $"{stats.owner}Owned";
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats != null && controllingCurrentCharacter)
        {
            if (Input.GetKey(KeyCode.Space))
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
        animator.SetBool("walking", vDirection != 0 || hDirection != 0);
        animator.SetFloat("directionH", hDirection);
        animator.SetFloat("directionV", vDirection);
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");

        updateMovementAnimation(hDirection, vDirection);
        transform.Translate(hDirection * speed * Time.deltaTime, 0, vDirection * speed * Time.deltaTime);
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
    }
    #endregion
}
