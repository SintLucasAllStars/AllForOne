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

    public float Timer { get { return timer; } }

    [Header("Stats")]
    public Stats stats;

    [HideInInspector] public int owner = 0;
    
    [Header("Configuration")]
    [SerializeField] private Transform hand = null;
    [SerializeField] private float speedRotation = 2;
    [SerializeField] private float speedMovement = 2;

    private Animator animator;
    private Weapon weapon;

    private bool isSelected = false;

    private float timer = 10;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if(isSelected)
        {
            if(timer >= 0)
            {
                timer -= Time.unscaledDeltaTime;

                float x = Input.GetAxis("Horizontal") * Time.deltaTime * 10 * speedRotation;
                float y = Input.GetAxis("Vertical");

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    y *= 2;
                }

                animator.SetFloat("Speed", y);
                transform.Rotate(0, x, 0);
            }
            else
            {
                DeSelect();
                animator.SetFloat("Speed", 0);
            }
        }
    }

    public void Select()
    {
        UnitUI.instance.DisplayUI(this, true);
        timer = 10;
        isSelected = true;
        UnitCamera.instance.SetUnitCamera(this);
    }

    public void DeSelect()
    {
        UnitUI.instance.DisplayUI(this, false);
        isSelected = false;
        UnitCamera.instance.SetPlayerCamera();
        GameManager.instance.NextTurn();
    }

    public void Hit(float damage)
    {
        if ((stats.health -= damage) <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Weapon _weapon = null;

        if (_weapon = other.GetComponent<Weapon>())
        {
            if (weapon)
                weapon.Unequip();

            weapon = _weapon;
            weapon.Equip(this, hand);
        }
            
    }
}
