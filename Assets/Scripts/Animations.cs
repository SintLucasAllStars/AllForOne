using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private Animator animator;
    private bool WPressed = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && !WPressed)
        {
            animator.SetBool("WPressed", true);
            WPressed = true;
            //Debug.Log("yr");
        }

        if (Input.GetKeyDown(KeyCode.W) && WPressed)
        {
            animator.SetBool("WPressed", false);
            WPressed = false;
        }

    }
}
