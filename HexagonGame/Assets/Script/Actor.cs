using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Transform CameraPoint;

    private Animator anim;
    private Rigidbody rb;
    private Warrior warrior;
    private bool paused;

    Weapon weapon;

    private float xCameraRotation;
    private float sensetivity;
    private bool isJumping;
    private float jumpVelocity;
     
    public void Start()
    {
        xCameraRotation = 0;
        sensetivity = 4;
        isJumping = false;
        jumpVelocity = 2;

        anim = this.GetComponent<Animator>();
        paused = false;
        rb = this.GetComponent<Rigidbody>();

        weapon.Damage = 2;
        weapon.speed = 10;
        weapon.range = 0;
        weapon.weaponType = WeaponType.Hand;
    }

    private void Update()
    {
        if (!warrior.GetIsSelected()) return;

        if (!paused)
        {
            Rotate();
            Jump();
            Input();
        }
    }

    private void FixedUpdate()
    {
        if (!warrior.GetIsSelected()) return;

        if (!paused)
        {
            Move();

            if (isJumping)
            {
                isJumping = false;
                rb.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.Impulse);
            }
        }
    }

    private void Move()
    {
        float horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        float vertical = UnityEngine.Input.GetAxisRaw("Vertical");

        Vector3 movementHorizontal = transform.right * horizontal;
        Vector3 movementVertical = transform.forward * vertical;

        Vector3 velocity = (movementHorizontal + movementVertical).normalized * warrior.GetSpeed();

        rb.MovePosition(transform.position + velocity * Time.deltaTime);
    }

    private void Rotate()
    {
        float YPlayerRotation = UnityEngine.Input.GetAxisRaw("Mouse X");
        transform.Rotate(0, YPlayerRotation * sensetivity, 0);
    }

    private void Jump()
    {
        RaycastHit hit;

        if (UnityEngine.Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2f))
            isJumping = true;
    }

    private void Input()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("Running", true);
        }
        if (UnityEngine.Input.GetKeyUp(KeyCode.W))
        {
            anim.SetBool("Running", false);
        }

        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            if(weapon.weaponType != WeaponType.Gun)
                anim.SetTrigger(weapon.weaponType.ToString() + "Trigger");

            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), this.transform.forward, out hit, (0.1f + weapon.range), 9))
            {
                hit.transform.GetComponent<Actor>().Hit(weapon.Damage);
            }
        }
    }

    public void Hit(float damage)
    {
        float health = warrior.GetHealth();

        if (health < damage)
        {
            Destroy(this.gameObject);
            PlayerControl.Instance.removeWarrior(this);
        } else
        {
            health -= damage;
            warrior.SetHealth(health);
        }

        anim.SetTrigger("Hit");
    }

    public void EndofTurn()
    {
        anim.SetBool("Running", false);           
    }

    public bool IsDeath()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 1.2f))
        {
            if (hit.transform.GetComponent<TileScript>().GetTileType() == TileType.Outside)
            {
                Destroy(this.gameObject);
                return true;
            }
        }

        return false;
    }

    public Warrior GetWarrior() { return warrior; }
    public void SetWarrior(Warrior a_Warrior) { this.warrior = a_Warrior; }

    public Transform GetCameraPoint() { return CameraPoint; }

}

struct Weapon
{
    public float Damage;
    public float range;
    public int speed;
    public WeaponType weaponType;
}

enum WeaponType
{
    Hand,
    Sword,
    Gun
}
