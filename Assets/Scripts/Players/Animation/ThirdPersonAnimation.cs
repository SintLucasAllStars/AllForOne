using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Players.Animation
{
    [RequireComponent(typeof(Animator))]
    public class ThirdPersonAnimation : MonoBehaviour
    {



        public AnimationCLips Clips;
        private Animator _animator;
        private AnimatorOverrideController _animatorOverrideController;
        private AnimationClipOverrides _clipOverrides;
        private WeaponMono _weaponMono;

        private CharacterMono _characterMono;
        private CapsuleCollider _capsuleCollider;
        private Rigidbody _rigidbody;


        [SerializeField] private GameObject _root;


        //when character dies
        [SerializeField] private float _colliderCenterOffsetY;

        [SerializeField] private AnimatorController _animatorGun;
        [SerializeField] private AnimatorController _animatorAxe;

        private RaycastHit _raycastHit;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _weaponMono = GetComponent<WeaponMono>();
            _characterMono = GetComponent<CharacterMono>();
            _capsuleCollider = GetComponent<CapsuleCollider>();
            _rigidbody = GetComponent<Rigidbody>();

        }


        private void Start()
        {
            InitializeAnimatorOverrideController();
            ApplyAnimations();
        }

        private void InitializeAnimatorOverrideController()
        {
            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;
            _clipOverrides = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
            _animatorOverrideController.GetOverrides(_clipOverrides);
        }

        public void FootL()
        {
            
        }

        public void FootR()
        {
            
        }
        

        private void ApplyAnimations()
        {
//            _clipOverrides["HangAnimation"] = Clips.HangAnimationClip;
//            _clipOverrides["Punch"] = Clips.PunchAnimationClip;
//            _clipOverrides["Die"] = Clips.DieAnimationClip;
//            _animatorOverrideController.ApplyOverrides(_clipOverrides);
        }



        private void Update()
        {

        }

        public void WeaponChanged(Weapon.WeaponEnum weaponEnum)
        {
            switch (weaponEnum)
            {
                case Weapon.WeaponEnum.Fists:
                    break;        
                case Weapon.WeaponEnum.Gloves:
                    break;
                case Weapon.WeaponEnum.Knife:
                    _clipOverrides["Uppercut"] = Clips.KnifeAttackAnimationClip;
                    _clipOverrides["idleLoco"] = Clips.KnifeIdleAnimationClip;

                     break;
                case Weapon.WeaponEnum.WarHammer:
                    _animator.runtimeAnimatorController = _animatorAxe;
                    InitializeAnimatorOverrideController();
                    break;
                case Weapon.WeaponEnum.Gun:
                    _animator.runtimeAnimatorController = _animatorGun;
                    InitializeAnimatorOverrideController();

                    break;
                default:
                    throw new ArgumentOutOfRangeException("weaponEnum", weaponEnum, null);
            }
            _animatorOverrideController.ApplyOverrides(_clipOverrides);
        }


        public void Fortify(bool value)
        {
            _animator.SetBool("Fortify", value);
        }
        
           



        public void Hit()
        {
            Vector3 rayCastPos = new Vector3(transform.position.x, transform.position.y + _capsuleCollider.center.y, transform.position.z);
            Vector3 forwards = new Vector3(transform.forward.x, transform.position.y , transform.forward.z);
            Ray myRay = new Ray(rayCastPos, forwards);
            Debug.DrawRay(myRay.origin,myRay.direction, Color.red);
            if (Physics.Raycast(myRay, out _raycastHit, _weaponMono.Range))
            {

                _characterMono.HandleAttack(_raycastHit);

            }
        }

        public void Die()
        {
            _animator.SetTrigger("Die");
            _capsuleCollider.enabled = false;
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = true;

        }

        public void SetPunchTrigger()
        {
            _animator.SetTrigger("Punch");
        }

        public bool IsPunching()
        {
            return _animator.GetCurrentAnimatorStateInfo(0).IsTag("Punching");
        }



        public void ResetForward()
        {
            _animator.SetFloat("Forward", 0f);
        }


    }

    [System.Serializable]
    public class AnimationCLips
    {
        public AnimationClip HangAnimationClip;
        public AnimationClip KnifeAttackAnimationClip;
        public AnimationClip KnifeIdleAnimationClip;
        
        public AnimationClip DieAnimationClip;




    }
}
