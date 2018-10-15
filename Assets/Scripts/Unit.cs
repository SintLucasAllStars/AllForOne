using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Unit : MonoBehaviour
{
    [System.Serializable]
    public struct Stats
    {
        public float Health     { get { return health; }            set { health = value; } }
        public float Strength   { get { return strength; }          set { strength = value; } }
        public float Speed      { get { return speed * 0.25f; }     set { speed = value; } }
        public float Defence    { get { return defence * 1.5f; }    set { defence = value; } }

        [SerializeField] private float health;
        [SerializeField] private float strength;
        [SerializeField] private float speed;
        [SerializeField] private float defence;
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

    private float cd_Attack = 0;

    public void Spawn()
    {
        owner.UnitCount += 1;
        print(owner.name + " Unit Count: " + owner.UnitCount);

        animator.SetFloat("MovementSpeed", 1 + stats.Speed);
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

                if(Input.GetButtonDown("Attack") && cd_Attack <= 0)
                {
                    if (weapon)
                    {
                        cd_Attack = weapon.Stats.Animation.length / weapon.Stats.Speed;
                    }  
                    else
                    {
                        cd_Attack = 1.867f / 3;

                        RaycastHit hit;

                        if(Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit))
                        {
                            if(hit.collider.GetComponent<Unit>())
                            {
                                hit.collider.GetComponent<Unit>().Hit(stats.Strength);
                            }
                        }
                    }

                    animator.SetTrigger("Attack");



                }

                if (cd_Attack > 0)
                    cd_Attack -= Time.deltaTime;
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
        if ((stats.Health -= (damage / stats.Defence)) <= 0)
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
        animator.SetFloat("AttackSpeed", weapon.Stats.Speed);
        animator.SetBool("Armed", true);
        aoc["Attack"] = weapon.Stats.Animation;
    }
}
