using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    private Rigidbody rb;

    public float speed;


    private void Start()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update ()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime / 10f);
        transform.Rotate(0, Input.GetAxis("Horizontal") * 130f * Time.deltaTime, 0);

    }

}
