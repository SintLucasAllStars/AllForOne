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
    

        private void Awake()
        {
            _animator = GetComponent<Animator>();    
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

        private void ApplyAnimations()
        {
            _clipOverrides["HangAnimation"] = Clips.HangAnimationClip;
            _clipOverrides["Punch"] = Clips.PunchAnimationClip;

            
            _animatorOverrideController.ApplyOverrides(_clipOverrides);
        }

        private void Update()
        {
        
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
        public AnimationClip PunchAnimationClip;
        public AnimationClip RunAnimationClip;
        public AnimationClip WalkAnimationClip;
        public AnimationClip IdleAnimationClip;
        public AnimationClip TurnRightAnimationClip;
        public AnimationClip TurnLeftAnimationClip;
    }
}