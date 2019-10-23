using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{

    public CameraMove cmScript;
    public GameObject gameUI;

    public bool switchPlayer;
    public bool scriptOn;

    public PlayerController pcScript;

    //public bool switchplayer;
    public Animator anim;

    public bool endGame = false;
    public TimeBalk tbScript;
    public ItemSpawn isScript;

    public GameObject playerNow;

    public bool testHit, oneunit = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && scriptOn)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                //pcScript = hit.transform.gameObject.GetComponent<PlayerController>();
                if (hit.transform.tag == "u_Player1" && !switchPlayer)
                {
                    tbScript.test = true;
                    oneunit = true;
                    //pcScript.canClick = true;
                    testHit = true;
                    playerNow = hit.transform.gameObject;
                    tbScript.walkOn = true;
                    anim = hit.transform.gameObject.GetComponent<Animator>();
                    Cursor.lockState = CursorLockMode.Locked;
                    pcScript = hit.transform.GetComponent<PlayerController>();
                    pcScript.speed = hit.transform.gameObject.GetComponent<UnitValues>().speed;

                    Debug.Log("yes");
                    cmScript.objectToFollow = hit.transform.GetChild(0).gameObject;
                    cmScript.pcScript = hit.transform.GetComponent<PlayerController>();
                    cmScript.CameraParent();
                }
                else if(hit.transform.tag == "u_Player2" && switchPlayer)
                {
                    tbScript.test = true;
                    oneunit = true;
                    //pcScript.canClick = true;
                    testHit = true;
                    playerNow = hit.transform.gameObject;
                    tbScript.walkOn = true;
                    anim = hit.transform.gameObject.GetComponent<Animator>();
                    Cursor.lockState = CursorLockMode.Locked;
                    pcScript = hit.transform.GetComponent<PlayerController>();
                    pcScript.speed = hit.transform.gameObject.GetComponent<UnitValues>().speed;


                    Debug.Log("yes");
                    cmScript.objectToFollow = hit.transform.GetChild(0).gameObject;
                    cmScript.pcScript = hit.transform.GetComponent<PlayerController>();
                    cmScript.CameraParent();
                }
            }
        }
    }

    public void StartGame()
    {
        cmScript.enabled = true;
        //cmScript.CameraParent();
        gameUI.SetActive(true);
    }

    public void SwitchPlayer()
    {
        if (!endGame)
        {
            //pcScript.canClick = false;
            switchPlayer = !switchPlayer;
            pcScript.SetAnimState();
            pcScript.AnimRemover();
            isScript.RespawnWeapons();
        }
    }
}
