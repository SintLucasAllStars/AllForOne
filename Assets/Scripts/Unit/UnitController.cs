using UnityEngine;

public class UnitController : MonoBehaviour
{
    private CharacterController characterController;
    private float JumpForce;
    private float speed = 2f;

    public bool isControlled = false;
    private void Start()
    {
        this.gameObject.AddComponent<CharacterController>();
        characterController = this.gameObject.GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        if (isControlled)
        {
            InputController();
        }
    }

    private void InputController()
    {
        var gravity = 20.0f;
        var jumpHeight = 20.0f;
        var moveDir = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal"));
        var rotateDir = new Vector3(0, Input.GetAxis("Rotate"), 0);

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed *= 1.5f;
        }
        if (Input.GetKeyDown(KeyCode.Space) && characterController.isGrounded)
        {
            moveDir.y = jumpHeight;
        }

        moveDir *= speed;

        //add the gravity
        moveDir.y -= gravity * Time.deltaTime;
        this.gameObject.transform.Rotate(rotateDir);
        characterController.Move(moveDir * Time.deltaTime);
    }

    public void CheckIfInside()
    {
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, Vector3.up, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.cyan);

            Destroy(this.gameObject);
            //Because the unit class is not implemted correctly the list will only grow of
        }
    }

    public void SetColor(bool isOne)
    {
        if (isOne)
        {
            gameObject.GetComponent<Renderer>().material.color = Unit.colors[0];
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = Unit.colors[1];
        }
    }
}