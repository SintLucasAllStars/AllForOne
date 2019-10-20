using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitcher : MonoBehaviour
{

    public CameraMove cmScript;
    public GameObject gameUI;

    public bool switchPlayer;
    public bool scriptOn;

    public bool switchplayer;

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
                if (hit.transform.tag == "u_Player1")
                {
                    Debug.Log("yes");
                    cmScript.objectToFollow = hit.transform.GetChild(0).gameObject;
                    cmScript.pcScript = hit.transform.GetComponent<PlayerController>();
                    cmScript.CameraParent();                }
                else
                {
                    Debug.Log("Not your Unit");
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
        switchPlayer = !switchPlayer;
    }
}
