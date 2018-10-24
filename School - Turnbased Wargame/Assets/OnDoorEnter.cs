using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoorEnter : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        if (anim == null)
            Destroy(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("character_nearby", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("character_nearby", false);
    }
}
