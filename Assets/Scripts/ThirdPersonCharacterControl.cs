using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ThirdPersonCharacterControl : MonoBehaviour
{

    public float strength;
    public float health;
    public float speed;
    public float defense;

    public Camera unitCam;

    public Collider col;

    private bool isControlled;

    private float rotationspeed = 100f;

    public LayerMask mask;

    public float Distance;

    public Canvas winner1;
    public Canvas winner2;
    bool isCreated;

    public AudioSource victory;

    void Start()
    {
        strength = GameInfo.Strength;
        health = GameInfo.Health;
        speed = GameInfo.Speed;
        defense = GameInfo.Defense;
    }

    void Update()
    {
        if (isControlled)
        {
            PlayerMovement();
            StartCoroutine(SwitchTime());
        } 

        if (Input.GetKey(KeyCode.P))
        {
            unitCam.enabled = false;
            isControlled = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(transform.position, forward, Color.red);
            Debug.Log("click");
        }

        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("WINNER");
        }

        Ray ray = new Ray(transform.position + transform.up * 1f , transform.forward);
        RaycastHit hitInfo;

        if (Physics.Raycast (ray, out hitInfo, 2, mask))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.green);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Destroy(hitInfo.collider.gameObject);
                HitTarget target =  hitInfo.collider.GetComponent<HitTarget>();
                if (target != null)
                {
                    target.TakeDamage(strength);
                }
            }

        }
        else
        {
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 2, Color.red);
        }
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Vector3 playerMovement = new Vector3(0f, 0f, 1f) * speed * Time.deltaTime;
            transform.Translate(playerMovement, Space.Self);
        }
        float rotation = Input.GetAxis("Horizontal") * rotationspeed;
        rotation *= Time.deltaTime;
        transform.Rotate(0, rotation, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("ATTACK");
        }

        CheckPlayers();
    }

    public void ControlUnit()
    {
        unitCam.enabled = true;
        col.enabled = true;
        isControlled = true;
    }

    void Test()
    {
        unitCam.enabled = false;
        col.enabled = false;
        isControlled = false;
    }

    IEnumerator SwitchTime()
    {
        yield return new WaitForSeconds(10f);
        unitCam.enabled = false;
        isControlled = false;
        Debug.Log("TURN ENDED");
        yield return null;
    }

    public void CheckPlayers()
    {

        if (GameObject.FindGameObjectsWithTag("Player 1").Length == 0)
        {
            Debug.Log("WE HAVE A WINNER!!!");
            Time.timeScale = 0;
            if (!isCreated)
            {
                Instantiate(victory);
                Instantiate(winner2);
                isCreated = true;
            }
        }

        if (GameObject.FindGameObjectsWithTag("Player 2").Length == 0)
        {
            Debug.Log("WE HAVE A WINNER!!!");
            Time.timeScale = 0;
            if (!isCreated)
            {
                Instantiate(victory);
                Instantiate(winner1);
                isCreated = true;
            }
        }
    }
}