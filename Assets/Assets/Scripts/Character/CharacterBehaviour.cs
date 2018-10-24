using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Item;
using UI;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using Debug = UnityEngine.Debug;

namespace Character
{
    public class CharacterBehaviour : Character
    {
        private Rigidbody rb;

        private Animator animator;

        private bool _grounded;

        public CapsuleCollider CapColl;

        private bool _attack;

        private bool inCharacterSelect = false;

        private bool canAttack = true;

        public GameObject weaponSlot;

        public GameObject[] weapons;

        private GameObject ActiveWep;

        private bool hasWepEquipped;
        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if (!inCharacterSelect)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {

                    animator.SetBool("jump", true);
                    ApplyJumpForce();
                    _grounded = false;
                    animator.SetBool("Grounded", _grounded);



                }

                /* if (expr)
                 {
                     
                 }*/
                if (Input.GetMouseButtonDown(0)&&canAttack)
                {
                    Attack();
                }
                
               
                float Hor = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;

                transform.position += transform.right * Hor;

                float Ver = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
                animator.SetFloat("Forward", Ver);
                animator.SetFloat("Strafe", Hor);
                transform.position += transform.forward * Ver;
            }


        }

        void Attack()
        {
            animator.SetBool("Attack", true);
            animator.speed = CalculateAttackSpeed(ActiveWep);
            canAttack = false;
        }

        void setFalse()
        {
            animator.SetBool("Attack", false);
            animator.speed = 1;
            canAttack = true;
        }

        void ToggleOnFistCollider()
        {
            Debug.Log("Active");
            CapColl.enabled = true;
        }

        void ToggleOffFistCollider()
        {
            Debug.Log("Deactive");
            CapColl.enabled = false;
            _attack = false;
        }

        void ApplyJumpForce()
        {
            rb.AddForce(new Vector3(0, jumpPower, 0));
        }



        void OnCollisionEnter(Collision coll)
        {
            if (coll.gameObject.CompareTag("Ground"))
            {
                _grounded = true;

                animator.SetBool("jump", false);
                animator.SetBool("Grounded", _grounded);

            }

            if (coll.gameObject.CompareTag("WeaponPickup"))
            {
                ActiveWep = weapons[1];
                AddToHand(coll.gameObject);
                hasWepEquipped = true;
            }

            if (coll.gameObject.CompareTag("Weapon"))
            {
                ChangeHealth(CheckWepDamage(coll.gameObject));
               
                    
            }

            if (coll.gameObject.CompareTag("Fist"))
            {
                
            }
        }

        public int CheckWepDamage(GameObject go)
        {
            int Damage =0;
            int PlayerStrength = Mathf.RoundToInt(Strength);
            int WepDamage = go.gameObject.GetComponent<WeaponBehaviour>().Damage;

            Damage = (PlayerStrength* WepDamage)/2;
            /*switch ()
            {
                case Weapon.WeaponType.Knife:

                    break;
                case Weapon.WeaponType.PowerPunch:
                    break;
                case Weapon.WeaponType.Warhammer:
                    break;
            }*/


            Debug.Log(Damage);
            return -Damage;
        }

        public float CalculateAttackSpeed(GameObject go)
        {
            float Speed = 1;
            if (hasWepEquipped)
            {
                float WepSpeed = go.GetComponent<WeaponBehaviour>().Speed;
                float CharacterSpeed = AttackSpeed;
                Speed = 3 ;
            }
            else
            {
                Speed = 1;
            }
            

            
            
            Debug.Log(Speed);
            return  Speed;
        }

        public void ChangeHealth(int value)
        {
            Health += value;
            Debug.Log(Health);
        }

        void AddToHand(GameObject go)
        {
            var temp = go.GetComponent<WeaponBehaviour>();

            switch (temp.Type)
            {
                case Weapon.WeaponType.Knife:
                    
                    animator.SetBool("IsArmed", true);
                    animator.SetInteger("WeaponType",1);
                    Destroy(go);
                    weapons[1].SetActive(true);
                    break;
                case Weapon.WeaponType.PowerPunch:

                    animator.SetBool("IsArmed", true);
                    animator.SetInteger("WeaponType", 0);
                    weapons[0].SetActive(true);
                    break;
                case Weapon.WeaponType.Gun:

                    animator.SetBool("IsArmed", true);
                    animator.SetInteger("WeaponType", 3);
                    weapons[3].SetActive(true);
                    break;
                case Weapon.WeaponType.Warhammer:

                    animator.SetBool("IsArmed", true);
                    animator.SetInteger("WeaponType", 2);
                    weapons[2].SetActive(true);
                    break;
            }
            CheckWepDamage(ActiveWep);
        }


    }
}    

