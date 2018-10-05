using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class ThirdPersonAnimation : MonoBehaviour
{

  

    public AnimationCLips Clips;

    private Animator _animator;
    protected AnimatorOverrideController AnimatorOverrideController;
    protected AnimationClipOverrides ClipOverrides;
    

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //InitializeAnimations();
        List<KeyValuePair<AnimationClip, AnimationClip>> overrides = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        //KeyValuePair<AnimationClip, AnimationClip> keyValuePair = new KeyValuePair<AnimationClip, AnimationClip>();  
        //overrides.Add(new KeyValuePair<AnimationClip, AnimationClip>(null, _runAnimation));
        AnimatorOverrideController.GetOverrides(overrides);
        //overrides[0] = new KeyValuePair<AnimationClip, AnimationClip>(overrides[0].Key,_jumpAnimation);
        Debug.Log(overrides);
        AnimatorOverrideController.ApplyOverrides(overrides);


    }

    private void Start()
    {
        
    }

    private void InitializeAnimatorOverrideController()
    {
        AnimatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
        _animator.runtimeAnimatorController = AnimatorOverrideController;
        ClipOverrides = new AnimationClipOverrides(AnimatorOverrideController.overridesCount);
        AnimatorOverrideController.GetOverrides(ClipOverrides);
        //ClipOverrides[""]
        

    }

    private void Update()
    {
        
    }

    public void SetSpeed(float speed)
    {
        _animator.SetFloat("Speed",speed, 0.1f, Time.deltaTime);
        AnimatorState hoi = new AnimatorState();
        
    }


}

[System.Serializable]
public class AnimationCLips
{
    public AnimationClip RunAnimationClip;
    public AnimationClip WalkAnimationClip;
    public AnimationClip IdleAnimationClip;
}

public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
{
    public AnimationClipOverrides(int capacity) : base(capacity)
    {
        
    }

    public AnimationClip this[string name]
    {
        get { return Find(x => x.Key.name.Equals(name)).Value; }
        set
        {
            int index = FindIndex(x => x.Key.name.Equals(name));
            if (index != -1)
            {
                this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);

            }
        }
    }
    
}
