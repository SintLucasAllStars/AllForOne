using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    private Rigidbody rb;
    private Warrior warrior;
    private bool paused;

    public void Start()
    {
        warrior = new Warrior();
        paused = false;
        rb = this.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!warrior.GetIsSelected()) return;

        if (!paused)
        {
            Move();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 movementHorizontal = transform.right * horizontal;
        Vector3 movementVertical = transform.forward * vertical;

        Vector3 velocity = (movementHorizontal + movementVertical).normalized * warrior.GetSpeed();

        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    public void SetWarrior(Warrior a_Warrior)
    {
        this.warrior = a_Warrior;
    }

}
