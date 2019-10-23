﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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
    RaycastHit hit2;

    public PlayerSwitcher psScript;
    public TimeBalk tbScript;

    bool startGame = false;

    public TMP_Text winText;

    public GameObject sword,sword2;

    bool win1, win2 = false;

    public bool weapon1, weapon2 = false;

    public ItemSpawn isSpawn;
    public CameraMove cmScript;

    public bool test= false;

    public bool canClick = false;

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

            //if (e_health <= 0)
            //{
            //    e_dead = true;
            //    e_Anim.SetBool("isDead", true);
            //    StartCoroutine(WaitForDeadAni());
            //}
            //else
            //{
                
            //}
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDistance))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                if (hit.transform.tag == "u_Player2" && !psScript.switchPlayer && canClick)
                {
                    startGame = true;
                    Debug.Log("Attack");
                    e_health = hit.transform.gameObject.GetComponent<UnitValues>().health;
                    e_strength = hit.transform.gameObject.GetComponent<UnitValues>().strength;
                    e_defense = hit.transform.gameObject.GetComponent<UnitValues>().defense;

                    e_dead = hit.transform.gameObject.GetComponent<UnitValues>().dead;

                    e_Anim = hit.transform.gameObject.GetComponent<Animator>();
                    //weapon1 = hit.transform.gameObject.GetComponent<UnitValues>().weapon;

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
                else if (hit.transform.tag == "u_Player1" && psScript.switchPlayer && canClick)
                {
                    Debug.Log("Attack");
                    e_health = hit.transform.gameObject.GetComponent<UnitValues>().health;
                    e_strength = hit.transform.gameObject.GetComponent<UnitValues>().strength;
                    e_defense = hit.transform.gameObject.GetComponent<UnitValues>().defense;

                    e_dead = hit.transform.gameObject.GetComponent<UnitValues>().dead;

                    e_Anim = hit.transform.gameObject.GetComponent<Animator>();
                    //weapon2 = hit.transform.gameObject.GetComponent<UnitValues>().weapon;

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

        ////if ((GameObject.FindGameObjectsWithTag("u_Player1").Length < 1 || e_dead == true) && startGame && psScript.switchPlayer)
        ////{
        ////    psScript.endGame = true;
        ////    tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
        ////    tbScript.dead = true;
        ////    Debug.Log("Player 2 WINS!");
        ////}
        ////if ((GameObject.FindGameObjectsWithTag("u_Player2").Length < 1 || e_dead == true) && startGame && !psScript.switchPlayer)
        ////{
        ////    psScript.endGame = true;
        ////    tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
        ////    tbScript.dead = true;
        ////    Debug.Log("Player 1 WINS!");
        ////}

        if (e_health <= 0)
        {
            psScript.testHit = false;
            //e_dead = true;
            e_Anim.SetBool("isDead", true);
            winText = GameObject.Find("Timer").GetComponent<TimeBalk>().playerTurnText;
            if (GameObject.FindGameObjectsWithTag("u_Player1").Length == 1 && psScript.switchPlayer)
            {
                e_dead = true;
                test = true;
                //tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
                winText.text = ("Player 2 won!");
                win2 = true;
                e_health = 10;
                StartCoroutine(WaitForDeadAni());
                Debug.Log("testttt1");
                psScript.endGame = true;
                tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
                tbScript.dead = true;
                //checker();
                //return;
            }
            if (GameObject.FindGameObjectsWithTag("u_Player2").Length == 1 && !psScript.switchPlayer)
            {
                e_dead = true;
                test = true;
                //tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
                winText.text = ("Player 1 won!");
                win1 = true;
                e_health = 10;
                StartCoroutine(WaitForDeadAni());
                Debug.Log("testttt2");
                psScript.endGame = true;
                tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
                tbScript.dead = true;
                //checker();
                //return;
            }
            else
            {
                if (test == false)
                {
                    e_health = 10;
                    StartCoroutine(WaitForDeadAni());
                    Debug.Log("testttt5");
                    //psScript.endGame = true;
                    //tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
                    //tbScript.dead = true;
                    //checker();
                }
            }
        }
        else
        {

        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit2, rayDistance))
        {
            Debug.Log(hit2.transform.gameObject.name);
            if (Input.GetKey("e") && hit2.transform.gameObject.name == "Sword_tex(Clone)")
            {
                if (!psScript.switchPlayer)
                {
                    cmScript = GameObject.Find("Main Camera").GetComponent<CameraMove>();
                    weapon1 = true;
                    sword = hit2.transform.gameObject;


                    //GameObject originalGameobject = psScript.playerNow.transform.gameObject;

                    Debug.Log(psScript.playerNow.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0));
                    sword.transform.parent = psScript.playerNow.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                    //sword.transform.parent = GameObject.Find("mixamorig:LeftHand").transform;
                    sword.transform.rotation = new Quaternion(0, 0, 0, 0);
                    sword.transform.localPosition = new Vector3(0, 0, 0);
                }
                if (psScript.switchPlayer)
                {
                    weapon2 = true;
                    sword2 = hit2.transform.gameObject;
                    sword2.transform.parent = psScript.playerNow.transform.GetChild(3).GetChild(2).GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0);
                    sword2.transform.rotation = new Quaternion(0, 0, 0, 0);
                    sword2.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
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
        tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
        walkOn = false;
        //psScript.endGame = true;
        yield return new WaitForSeconds(4);
        psScript.endGame = false;
        Destroy(enemy);
        tbScript.slider.value = 0;
        tbScript.timeRemaining = 0;
        e_health = 1;
        Debug.Log("op");
        if (win1 == true)
        {
            Debug.Log("op2");
            Player1Win();
        }
        else if (win2 == true)
        {
            Debug.Log("op3");
            Player2Win();
        }
        Debug.Log("dasasasasasasasasasasasasasasasasasas");
        //tbScript.timeRemaining = 10;
    }
    IEnumerator WaitForDeadAni2()
    {
        walkOn = false;
        //psScript.endGame = true;
        yield return new WaitForSeconds(4);
        Destroy(enemy);
        e_health = 1;
        Debug.Log("op");
        if (win1 == true)
        {
            Debug.Log("op2");
            Player1Win();
        }
        else if (win2 == true)
        {
            Debug.Log("op3");
            Player2Win();
        }
        Debug.Log("dasasasasasasasasasasasasasasasasasas");
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
        if (e_Anim.GetBool("isReady") == true && (weapon1 || weapon2))
        {
            e_health -= (this.GetComponent<UnitValues>().strength / e_defense + 1);
            hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
            Debug.Log(1);
            weapon1 = false;
            weapon2 = false;
            return;
        }
        else if (e_Anim.GetBool("isReady") == false && (weapon1 || weapon2))
        {
            e_health -= (this.GetComponent<UnitValues>().strength / 10 * 3);
            hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
            Debug.Log(2);
            weapon1 = false;
            weapon2 = false;
            return;
        }

        if (e_Anim.GetBool("isReady") == true && (!weapon1 || !weapon2))
        {
            e_health -= (this.GetComponent<UnitValues>().strength / e_defense);
            hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
            Debug.Log(3);
            weapon1 = false;
            weapon2 = false;
            return;
        }
        else if (e_Anim.GetBool("isReady") == false && (!weapon1 || !weapon2))
        {
            e_health -= (this.GetComponent<UnitValues>().strength / 10 * 2);
            hit.transform.gameObject.GetComponent<UnitValues>().health = e_health;
            Debug.Log(4);
            weapon1 = false;
            weapon2 = false;
            return;
        }
    }

    public void AnimRemover()
    {
        //e_Anim.SetBool("isHit", false);
        //e_Anim = null;
        enemy = null;
    }

    public void Player1Win()
    {
        SceneManager.LoadScene("Player 1 win");
    }
    public void Player2Win()
    {
        SceneManager.LoadScene("Player 2 win");
    }

    public void checker()
    {
        if ((GameObject.FindGameObjectsWithTag("u_Player1").Length < 1 || e_dead == true) && startGame && psScript.switchPlayer)
        {
            psScript.endGame = true;
            tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
            tbScript.dead = true;
            Debug.Log("Player 2 WINS!");
        }
        if ((GameObject.FindGameObjectsWithTag("u_Player2").Length < 1 || e_dead == true) && startGame && !psScript.switchPlayer)
        {
            psScript.endGame = true;
            tbScript = GameObject.Find("Timer").GetComponent<TimeBalk>();
            tbScript.dead = true;
            Debug.Log("Player 1 WINS!");
        }
    }
}
