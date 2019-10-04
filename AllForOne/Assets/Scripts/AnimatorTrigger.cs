using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorTrigger : MonoBehaviour
{
    [SerializeField]
    private Soldier _soldier;

    public void AnimationAttack()
    {
        _soldier.Attack();
    }
}
