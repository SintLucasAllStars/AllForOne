using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Item;
using UI;
using UnityEditorInternal;
using UnityEngine;
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

        private bool inCharacterSelect = true;

        private bool canAttack = true;

        public GameObject weaponSlot;

        public GameObject[] weapons;

        private GameObject ActiveWep;

        private bool hasWepEquipped = false;

        private MouseAim mouseAim;

        public GameObject ThirdCamera;

        private bool isOutside = false;

        private bool fortify;

        private bool canFortify = true;
        // Use this for initialization
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
           ThirdCamera.SetActive(false);
            //ThirdCamera = Camera.current;

        }

        public void FortifyFalse()
        {
            fortify = false;
            canFortify = true;
            animator.SetBool("Fortify",false);
        }

        void SetHitFalse()
        {
            animator.SetBool("GetHit",false);
        }
        // Update is called once per frame
        void Update()
        {
            
            if (!inCharacterSelect)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !fortify)
                {

                    animator.SetBool("jump", true);
                    ApplyJumpForce();
                    _grounded = false;
                    animator.SetBool("Grounded", _grounded);



                }

                if (Input.GetKeyDown(KeyCode.F)&&canFortify)
                {
                    canFortify = false;
                    fortify = true;
                    animator.SetBool("Fortify",true);
                    GameManager.instance.NextTurn(GameManager.instance.GetCurrentPlayer());

                }

                if (!fortify)
                {
                    if (Input.GetMouseButtonDown(0) && canAttack && _grounded)
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

        void SetActiveWepCollider()
        {
            ActiveWep.GetComponent<BoxCollider>().enabled=true;
        }

        void SetInactiveWepCollider()
        {
            ActiveWep.GetComponent<BoxCollider>().enabled =false;
        }

        void ToggleOnFistCollider()
        {
            
            CapColl.enabled = true;
        }

        void ToggleOffFistCollider()
        {
            
            CapColl.enabled = false;
            _attack = false;
        }

        void ApplyJumpForce()
        {
            rb.AddForce(new Vector3(0, jumpPower, 0));
        }

        public void EnableControls()
        {
            inCharacterSelect = false;
        }

        public void DisableControls()
        {
            inCharacterSelect = true;
            animator.SetFloat("Forward", 0);
            animator.SetFloat("Strafe", 0);
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
                if (!hasWepEquipped)
                {
                    ActiveWep = weapons[1];
                    AddToHand(coll.gameObject);
                    hasWepEquipped = true;
                }
                
            }

            

            
        }

        public void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("OutsideArea"))
            {

                isOutside = true;
            }
            if (coll.CompareTag("Weapon"))
            {

                ChangeHealth(CheckWepDamage(coll.gameObject));
                animator.SetBool("GetHit", true);


            }
            if (coll.CompareTag("Fist"))
            {
                ChangeHealth(CheckWepDamage(coll.gameObject));
                animator.SetBool("GetHit", true);

            }
        }

        public void OnTriggerExit(Collider coll)
        {
            if (coll.CompareTag("OutsideArea"))
            {

                isOutside = false;
            }
        }

        public bool IsOutside()
        {
            return isOutside;
        }

        public void Die()
        {
            GameManager.instance.RemovePlayer(gameObject,WhichSide);
            
            
            Destroy(gameObject);
            GameManager.instance.CheckIfUnitsLeft();
        }

        public int CheckWepDamage(GameObject go)
        {
            int Damage =0;
            int PlayerStrength = Mathf.RoundToInt(Strength);

                int WepDamage = go.gameObject.GetComponent<WeaponBehaviour>().Damage;

                Damage = (PlayerStrength * WepDamage) / 2;
            



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
                Speed = (WepSpeed + AttackSpeed)/10 ;
            }
            else
            {
                Speed = 1;
            }
            return  Speed;
        }

        public void ChangeHealth(int value)
        {
            value -= Mathf.RoundToInt(Defense) ;
            if (value < 10)
            {
                value = -10;
            }

            if (fortify)
            {
                value= value /2;
            }
           Health += value;
            if (Health<= 0)
            {
                Die();
            }
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
            //CheckWepDamage(ActiveWep);
        }


    }
}    

