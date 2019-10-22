using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField]
    private Transform cameraPoint;
    [SerializeField]
    private Transform healthBar;
    [SerializeField]
    private Image healthImage;
    [SerializeField]
    private Image attackDelayImage;
    [SerializeField]
    private GameObject HighLightObj;
    [SerializeField]
    private TMP_Text weaponText;

    private Camera cam;
    private Animator anim;
    private Rigidbody rb;
    private Warrior warrior;
    private bool paused;

    private Item item;

    private float xCameraRotation;
    private float sensetivity;
    private bool isJumping;
    private float jumpVelocity;
    private bool CanAttack;
     
    public void Start()
    {
        item = new Item(1, 10, 0, "Punch", new GameObject(), WeaponType.Hand);
        xCameraRotation = 0;
        sensetivity = 4;
        isJumping = false;
        jumpVelocity = 2;
        CanAttack = true;

        anim = this.GetComponent<Animator>();
        paused = false;
        rb = this.GetComponent<Rigidbody>();
        cam = PlayerControl.Instance.GetCam();
    }

    private void Update()
    {
        HealthBarUpdate();
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

    private void HealthBarUpdate()
    {
        healthBar.transform.LookAt(cam.transform);
    }

    private void Move()
    {
        float horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");
        float vertical = UnityEngine.Input.GetAxisRaw("Vertical");

        Vector3 movementHorizontal = transform.right * horizontal;
        Vector3 movementVertical = transform.forward * vertical;

        Vector3 velocity = (movementHorizontal + movementVertical).normalized *  (1 + warrior.GetSpeed() / 30);

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
            if (CanAttack == true) {
                StartCoroutine(SetAttackDelay());
                if (item.GetWeaponType() != WeaponType.Gun)
                    anim.SetTrigger(item.GetWeaponType().ToString() + "Trigger");

                RaycastHit hit;
                if (Physics.Raycast(transform.position + new Vector3(0, 0.05f, 0), this.transform.forward, out hit, (0.1f + item.GetRange()), 9))
                {
                    hit.transform.GetComponent<Actor>().Hit((item.GetDamage() + warrior.GetStrenth()) / 15);
                }
            }
        }
    }

    private IEnumerator SetAttackDelay()
    {
        CanAttack = false;
        float time = 0;
        float fullDelayBar = 100;
        while (time < 10f / item.GetSpeed())
        {
            yield return new WaitForSeconds(0.5f);
            time += 0.5f;
            attackDelayImage.fillAmount = ((fullDelayBar / (10f / item.GetSpeed())) * time) / fullDelayBar;
        }
        CanAttack = true;
    }

    public void HighLight(bool a_Highlighted, Color a_Color)
    {
        HighLightObj.GetComponent<MeshRenderer>().material.SetColor("_BaseColor", a_Color);
        HighLightObj.SetActive(a_Highlighted);
    }   

    public void Hit(float damage)
    {
        float blockDevider = 60;
        float health = warrior.GetHealth();
        float trueDamage = damage - (warrior.GetDefense() / blockDevider);

        if (health < damage)
        {
            Destroy(this.gameObject);
            PlayerControl.Instance.removeWarrior(this);
        } else
        {
            float fullHealthBar = 100;
            health -= damage;
            float healthBarHealth = (fullHealthBar / warrior.GetMaxHealth()) * health;
            healthImage.fillAmount = (healthBarHealth / fullHealthBar);
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

    public void SetItem(Item a_Item) { item = a_Item; weaponText.text = item.GetName(); }

    public Transform GetCameraPoint() { return cameraPoint; }
}
