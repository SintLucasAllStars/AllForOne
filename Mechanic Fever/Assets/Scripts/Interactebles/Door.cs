using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour, ITriger
{
    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        tag = "Trigger";
    }

    void ITriger.OnActivate()
    {
        animator.SetBool("character_nearby", true);
    }

    void ITriger.OnDeactivate()
    {
        animator.SetBool("character_nearby", false);
    }
}
