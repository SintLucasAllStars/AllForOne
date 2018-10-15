using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public float health;
        public float strength;
        public float speed;
        public float defence;
    }

    public float Timer { get { return playTime; } }

    [Header("Stats")]
    public Stats stats;
    
    [Header("Configuration")]
    [SerializeField] private Transform hand = null;
    [SerializeField] private float speedRotation = 2;

    [HideInInspector] public Player owner;


    private Animator animator;
    private AnimatorOverrideController aoc;
    private Weapon weapon;

    private float playTime = 10;

    private bool isSelected = false;

    

    public void Spawn()
    {
        owner.UnitCount += 1;
        print(owner.name + " Unit Count: " + owner.UnitCount);
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = aoc;
    }

    void Update()
    {
        if(isSelected)
        {
            if(playTime >= 0)
            {
                playTime -= Time.unscaledDeltaTime;

                float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10 * speedRotation;
                float y = Input.GetAxis("Vertical");

                animator.SetFloat("Y", y);
                transform.Rotate(0, x, 0);

                if(Input.GetButtonDown("Attack"))
                {
                    animator.SetTrigger("Attack");
                }
            }
            else
            {
                DeSelect();
                animator.SetFloat("Y", 0);
            }
        }
    }

    public void Select()
    {
        UnitUI.instance.DisplayUI(this, true);
        playTime = 10;
        isSelected = true;
        GlobalCamera.instance.SetUnitCamera(this);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void DeSelect()
    {
        UnitUI.instance.DisplayUI(this, false);
        isSelected = false;
        GlobalCamera.instance.SetPlayerCamera();
        GameManager.instance.NextTurn();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Hit(float damage)
    {
        if ((stats.health -= damage) <= 0)
            Die();
    }

    public void Die()
    {
        owner.UnitCount -= 1;
        print(owner.name + " Unit Count: " + owner.UnitCount);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isSelected)
        {
            Weapon _weapon = null;

            if (_weapon = other.GetComponent<Weapon>())
            {
                if (weapon && weapon != _weapon)
                    weapon.Unequip();

                weapon = _weapon;
                weapon.Equip(this, hand);
                EquipWeapon();
            }
        }   
    }

    private void EquipWeapon()
    {
        animator.SetBool("Armed", true);
        aoc["Attack"] = weapon.Stats.Animation;
    }
}
