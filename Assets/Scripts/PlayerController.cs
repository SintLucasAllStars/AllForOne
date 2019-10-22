using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    public bool walkOn;

    public float e_health, e_strength, e_defense = 1f;
    public bool e_dead;
    public Animator e_Anim;
    public GameObject enemy;

    public int rayDistance;

    RaycastHit hit;

    public PlayerSwitcher psScript;

    bool startGame = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        psScript = GameObject.Find("GameManager").GetComponent<PlayerSwitcher>();
        if (walkOn)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //Vector3 targetDirection = new Vector3(moveHorizontal, 0f, moveVertical);
            //targetDirection = Camera.main.transform.TransformDirection(targetDirection);
            //targetDirection.y = 0.0f;

            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey("w"))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed * 2.5f;
            }
            else if (Input.GetKey("w") && !Input.GetKey(KeyCode.LeftShift))
            {
                transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
            }
            else if (Input.GetKey("s"))
            {
                transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * speed;
            }

            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * speed;
            }
            else if (Input.GetKey("d") && !Input.GetKey("a"))
            {
                transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * speed;
            }

            if(Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
            {
                psScript.anim.SetBool("isWalking", true);
            }
            else
            {
                psScript.anim.SetBool("isWalking", false);
            }

            if (Input.GetKey("x"))
            {
                psScript.anim.SetBool("isReady", true);
            }
            else
            {
                psScript.anim.SetBool("isReady", false);
            }

            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            transform.Rotate(0, h, 0);

            //this.transform.position += Vector3.Normalize(targetDirection);
            //rb.AddForce(targetDirection * speed);
            if (e_health <= 0)
            {
                e_dead = true;
                e_Anim.SetBool("isDead", true);
                StartCoroutine(WaitForDeadAni());
            }
            else
            {
                
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.transform.tag == "u_Player2" && !psScript.switchPlayer)
                {
                    startGame = true;
                    Debug.Log("Attack");
                    e_health = hit.transform.gameObject.GetComponent<UnitValues>().health;
                    e_strength = hit.transform.gameObject.GetComponent<UnitValues>().strength;
                    e_defense = hit.transform.gameObject.GetComponent<UnitValues>().defense;

                    e_dead = hit.transform.gameObject.GetComponent<UnitValues>().dead;

                    e_Anim = hit.transform.gameObject.GetComponent<Animator>();

                    enemy = hit.transform.gameObject;

                    //e_health -= (this.GetComponent<UnitValues>().strength / e_defense);

                    //hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;

                    psScript.anim.SetBool("isPunching", true);
                    e_Anim.SetBool("isHit", true);

                    //psScript.anim.ResetTrigger("isPunching");
                    //e_Anim.ResetTrigger("isHit");
                }
                //else
                //{
                //    psScript.anim.SetBool("isPunching", false);
                //    e_Anim.SetBool("isHit", false);
                //}
                else if (hit.transform.tag == "u_Player1" && psScript.switchPlayer)
                {
                    Debug.Log("Attack");
                    e_health = hit.transform.gameObject.GetComponent<UnitValues>().health;
                    e_strength = hit.transform.gameObject.GetComponent<UnitValues>().strength;
                    e_defense = hit.transform.gameObject.GetComponent<UnitValues>().defense;

                    e_dead = hit.transform.gameObject.GetComponent<UnitValues>().dead;

                    e_Anim = hit.transform.gameObject.GetComponent<Animator>();

                    enemy = hit.transform.gameObject;

                    //e_health -= (this.GetComponent<UnitValues>().strength / e_defense);

                    //hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;

                    psScript.anim.SetBool("isPunching", true);
                    e_Anim.SetBool("isHit", true);
                }
                //else
                //{
                //    psScript.anim.SetBool("isPunching", false);
                //    e_Anim.SetBool("isHit", false);
                //}
            }
        }

        if ((GameObject.FindGameObjectsWithTag("u_Player1").Length < 1 || e_dead == true) && startGame && psScript.switchPlayer)
        {
            psScript.endGame = true;
            Debug.Log("Player 2 WINS!");
        }

        if ((GameObject.FindGameObjectsWithTag("u_Player2").Length < 1 || e_dead == true) && startGame && !psScript.switchPlayer)
        {
            psScript.endGame = true;
            Debug.Log("Player 1 WINS!");
        }
    }

    public void SetAnimState()
    {
        if (psScript.anim.GetBool("isReady") == false)
        {
            psScript.anim.SetBool("isWalking", false);
        }
    }

    IEnumerator WaitForDeadAni()
    {
        yield return new WaitForSeconds(4);
        Destroy(enemy);
        e_health = 1;
    }

    IEnumerator WaitForPunch()
    {
        yield return new WaitForSeconds(3);
        //psScript.anim.SetBool("isPunching", false);
        //e_Anim.SetBool("isHit", false);
    }

    public void EndEvent()
    {
        psScript.anim.SetBool("isPunching", false);
        e_Anim.SetBool("isHit", false);
        e_health -= (this.GetComponent<UnitValues>().strength / e_defense);
        hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
    }

    public void AnimRemover()
    {
        e_Anim.SetBool("isHit", false);
        e_Anim = null;
        enemy = null;
    }
}
