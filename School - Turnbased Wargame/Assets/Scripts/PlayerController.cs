using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;

    private Rigidbody rb;

    public float speed;
    public ushort defaultStrength;


    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update ()
    {
        transform.Translate(0, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime / 10f);
        transform.Rotate(0, Input.GetAxis("Horizontal") * 130f * Time.deltaTime, 0);


        Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

        if (Input.GetMouseButtonDown(0))
        {
            //Check cooldown attack

            RaycastHit hit;
            if (Physics.Raycast (transform.position, transform.forward, out hit, 10f))
            {

              //  Debug.Log("Tag to " + hit.collider.name);

                Character enemy = hit.collider.GetComponent<Character>();
                if (enemy != null)
                {
                    enemy.TakeDamage(defaultStrength);
                    Debug.Log("Enemy found!");
                }
            }


        }

    }

}
