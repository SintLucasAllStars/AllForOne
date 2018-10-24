using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour, ITriger
{
    private Animator animator;
    int characters = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
        tag = "Trigger";
    }

    void ITriger.OnActivate()
    {
        animator.SetBool("character_nearby", true);
        characters++;
    }

    void ITriger.OnDeactivate()
    {
        characters--;
        if(characters == 0)
            animator.SetBool("character_nearby", false);
    }
}
