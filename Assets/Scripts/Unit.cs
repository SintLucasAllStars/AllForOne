using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Rigidbody rb;
    private Transform targetTransform;
    private float lookDirX;
    private float lookDirY;

    private float m_Health;
    private float m_Strength;
    private float m_Defence;
    private float m_Speed;
    [SerializeField] private Team m_Team;
    [SerializeField] private Weapon weapon;

    private Vector3 unitPos;
    private float attackDist = 2f;
    private float jumpForce = 7f;
    private bool controllable = true;
    private bool fortified;
    private float rageDamage;
    private bool raged;
    private float adrenalineSpeed;
    private bool adrenaline;

    public bool isControllable
    {
        get { return controllable; }
        set { controllable = value; }
    }

    public bool isFortified
    {
        get { return fortified; }
        set { fortified = value; }
    }

    public bool isRaged
    {
        get { return raged; }
        set
        {
            raged = value;
            rageDamage = raged ? (m_Strength * 0.1f) : 0.0f;
        }
    }

    public bool isAdrenaline
    {
        get { return adrenaline; }
        set
        {
            adrenaline = value;
            adrenalineSpeed = adrenaline ? (m_Speed * 0.5f) : 0.0f;
        }
    }

    public void SetUp(int health, int strength, int defence, int speed, Team team)
    {
        m_Health = health;
        m_Strength = strength;
        m_Defence = defence;
        m_Speed = speed;
        m_Team = team;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetTransform = transform.GetChild(0);
        lookDirX = transform.eulerAngles.y;

        SetUp(30, 15, 8, 500, m_Team); //<-toRemove
    }

    public void Move(Vector3 moveDir)
    {
        unitPos = moveDir * ((m_Speed + adrenalineSpeed) * Time.fixedDeltaTime);
        unitPos.y += rb.velocity.y;
        unitPos = transform.TransformDirection(unitPos);
        if (rb.velocity.y < 0)
        {
            unitPos.y += Physics.gravity.y * Time.fixedDeltaTime * 2.5f;
        }
        rb.velocity = unitPos;
    }

    public void Jump()
    {
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y), ForceMode.Impulse);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 1.1f);
    }

    public Vector3 GetCameraPos()
    {
        Vector3 pos = targetTransform.position - (transform.forward * 5.0f);
        return pos;
    }

    public void ResetTarget()
    {
        targetTransform.rotation = transform.rotation;
        lookDirY = 0;
        rb.mass = 100f;
    }

    public Transform GetTarget()
    {
        return targetTransform;
    }
    
    public float GetWeaponSpeed()
    {
        return weapon.speed;
    }

    public Team GetTeam()
    {
        return m_Team;
    }

    public void Look(float mouseX, float mouseY)
    {
        lookDirX += mouseX;
        lookDirY -= mouseY;
        lookDirY = Mathf.Clamp(lookDirY, -20, 60);
        rb.rotation = Quaternion.Euler(new Vector3(0, lookDirX, 0));
        targetTransform.rotation = Quaternion.Euler(new Vector3(lookDirY, lookDirX, 0));
    }

    public bool Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, attackDist + weapon.range))
        {
            Unit unit = hit.collider.GetComponent<Unit>();
            if (unit != null)
            {
                if(unit.GetTeam() != GetTeam())
                {
                    Debug.Log("Hit an enemy unit");
                    float damage = m_Strength * weapon.damage + rageDamage;
                    unit.Hit(damage, (hit.point - transform.position).normalized);

                    return true;
                }
            }
        }
        return false;
    }

    public void Hit(float damage, Vector3 direction)
    {
        if (fortified)
        {
            damage -= m_Defence;
            if(damage < 0)
            {
                damage = 0;
            }
        }

        m_Health -= damage;
        if(m_Health <= 0)
        {
            Destroy(gameObject);
        }

        rb.AddForce(direction * 10f, ForceMode.VelocityChange);

        //make invulnerable for another attack.
    }

    public void SetTeam(Team team, Material teamMat)
    {
        m_Team = team;
        GetComponent<Renderer>().material = teamMat;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Weapon"))
        {
            weapon = col.GetComponent<PickUpWeapon>().PickUp();
        }
        else if (col.CompareTag("PowerUp"))
        {
            PowerUp power = col.GetComponent<PickUpPower>().PickUp();
            FindObjectOfType<Player>().GetPower(power);
        }
    }
}

public enum Team { Red, Blue }