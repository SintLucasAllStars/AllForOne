using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]

public class Jump : MonoBehaviour {

    Rigidbody Rigidbody;

    public Collider _collider;

    private Movement _movement;

    private bool _canJump = true;

    public void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        _movement = GetComponent<Movement>();
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _collider.bounds.extents.y + 0.1f);
    }

    public void PerformJump()
    {
        if (IsGrounded() && !_movement.InAir && _canJump)
        {
            StartCoroutine(JumpDelay());
            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, 8, Rigidbody.velocity.z);
            _movement.Jump();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PerformJump();
        }
    }

    private IEnumerator JumpDelay()
    {
        _canJump = false;
        yield return new WaitForSeconds(1);
        _canJump = true;
    }

}