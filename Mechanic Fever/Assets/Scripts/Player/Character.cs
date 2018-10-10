using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Character : MonoBehaviour
{
    private const int maxPoints = 100;
    private const int minPoints = 10;

    [Header("Activation")]
    private ThirdPersonUserControl controller;
    private ThirdPersonCharacter character;

    private float health;

    [SerializeField] private Vector2 damageRange;
    private float strength;

    private float defence;

    [HideInInspector] public bool isFortified;

    private Rigidbody playeRigidbody;

    [Header("Camera")]
    [SerializeField] private GameObject cameraGameObject;
    public Transform pivot;
    public Vector3 cameraLocation;

    private void Start()
    {
        controller = GetComponent<ThirdPersonUserControl>();
        character = GetComponent<ThirdPersonCharacter>();
        GameManager.instance.EndRound += EndRound;
        playeRigidbody = GetComponent<Rigidbody>();
    }

    public void SetStats(float health, float strength, float speed, float defence)
    {
        this.health = health;
        this.strength = strength;
        controller.runSpeed = speed;
        this.defence = defence;
        GetComponent<CapsuleCollider>().enabled = true;
    }

    void EndRound()
    {
        GameManager.instance.EndRound -= EndRound;
        ActivateCharacter(false);
    }


    public void ActivateCharacter(bool activate)
    {
        cameraGameObject.SetActive(activate);
        controller.enabled = activate;
        character.enabled = activate;
        playeRigidbody.isKinematic = !activate;

        if(activate)
            GameManager.instance.EndRound += EndRound;
        else
            ResetAnimator();
    }

    private void ResetAnimator()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetFloat("Forward", 0);
        animator.SetFloat("Turn", 0);
        animator.SetFloat("Jump", 0);
        animator.SetFloat("JumpLeg", 0);
        animator.SetBool("OnGround", true);
        animator.SetBool("Crouch", false);
    }

    public void Attack()
    {
        int damage = Mathf.RoundToInt(Mathf.Lerp(damageRange.x, damageRange.y, strength));
        //Raycast :)
    }

    public void Damage(int damage)
    {
        health -= (isFortified) ? damage - damage / 2 * (1 - defence) : damage;
        if(health <= 0)
        {
            GameManager.instance.KillCharacter();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnActivate();
        }
        else if(other.CompareTag("PickUp"))
        {
            Debug.Log("PickUp");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Trigger"))
        {
            other.GetComponent<ITriger>().OnDeactivate();
        }
    }
}
