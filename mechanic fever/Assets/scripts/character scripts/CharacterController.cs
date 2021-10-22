using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterController : MonoBehaviour
{
    private Animator Animator;
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
    }

    private void Start()
    {
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (stats != null)
        {
            Movement();
        }
    }

    private void Movement()
    {
        float hDirection = Input.GetAxis("Horizontal") * Time.deltaTime;
        float vDirection = Input.GetAxis("Vertical") * Time.deltaTime;

        Animator.SetBool("walking", vDirection != 0 || hDirection != 0);
        transform.Translate(hDirection * speed, 0, vDirection * speed);
    }
}
