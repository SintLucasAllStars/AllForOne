using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    public Animator unitAnimatior;


    /// <summary>
    /// When called play a single animation.
    /// </summary>
    public void PlaySingle(string name)
    {
        unitAnimatior.Play(name, -1, 0);
    }

    /// <summary>
    /// Call when switching from Run to Idle. True for Run False for Idle.
    /// </summary>
    public void AnimMove(bool isMoving)
    {
        unitAnimatior.SetBool("Run", isMoving);
    }

    /// <summary>
    /// Call when switching from Attacking to Idle. True for attack False for Idle.
    /// </summary>
    public void AnimAttack(bool isAttacking)
    {
        unitAnimatior.SetBool("Attack", isAttacking);
    }
}
