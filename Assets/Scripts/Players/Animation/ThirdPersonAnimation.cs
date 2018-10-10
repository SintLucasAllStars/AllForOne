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

            
            _animatorOverrideController.ApplyOverrides(_clipOverrides);
        }

        private void Update()
        {
        
        }

        public void SetSpeed(float speed)
        {
            _animator.SetFloat("Speed",speed, 0.1f, Time.deltaTime);        
        }

        public void SetXInput(float xInput)
        {
            _animator.SetFloat("Xinput",xInput, 0.1f, Time.deltaTime);        
        }

        public void SetPlacementFinished(bool input)
        {
            //_animator.SetBool("Grounded", input);
        }


    }

    [System.Serializable]
    public class AnimationCLips
    {
        public AnimationClip HangAnimationClip;
//        public AnimationClip HumanoidCrouchIdleAnimationClip;
//        public AnimationClip HumanoidCrouchWalkAnimationClip;
//        public AnimationClip HumanoidCrouchWalkLeftAnimationCLip;
//        public AnimationClip HumanoidCrouchWalkRightBAnimationClip;
//        public AnimationClip HumanoidFallAnimationClip;
//        public AnimationClip HumanoidIdleAnimationClip;
//        public AnimationClip HumanoidJumpForwardLeftAnimationClip;
//        public AnimationClip HumanoidJumpForwardRightAnimationClip;
//        public AnimationClip HumanoidJumpUpAnimationClip;
//        public AnimationClip 
        
        
        
        public AnimationClip RunAnimationClip;
        public AnimationClip WalkAnimationClip;
        public AnimationClip IdleAnimationClip;
        public AnimationClip TurnRightAnimationClip;
        public AnimationClip TurnLeftAnimationClip;
    }
}