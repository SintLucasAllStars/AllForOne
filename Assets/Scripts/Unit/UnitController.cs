using UnityEngine;

public class UnitController : MonoBehaviour
{
    private CharacterController characterController;
    private float JumpForce;
    float speed = 2f;
    private void Start()
    {
        this.gameObject.AddComponent<CharacterController>();
        characterController = this.gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        InputController();
    }

    private void InputController()
    {
        if (characterController.isGrounded)
        {
            var gravity = 20.0f;
            var jumpHeight = 8.0f;
            var moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.LeftShift))
                speed *= 1.5f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = jumpHeight;
            }

            moveDir *= speed;

            //add the gravity
            moveDir.y -= gravity * Time.deltaTime;
            characterController.Move(moveDir * Time.deltaTime);
        }
    }
}