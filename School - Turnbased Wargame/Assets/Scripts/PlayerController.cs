using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Animator anim;

    private CharacterController control;
    public float speed;


    private void Start()
    {
        control = GetComponent<CharacterController>();
    }

    void Update ()
    {
        Vector3 move = new Vector3(0, -0.1f, Input.GetAxis("Vertical"));  ///eerste horizontal
        control.Move(move * Time.deltaTime *  speed / 5f);
    }

}
