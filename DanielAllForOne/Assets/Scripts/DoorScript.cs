using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            anim.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            anim.SetBool("character_nearby", false);
        }
    }
}
