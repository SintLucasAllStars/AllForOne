using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    MeshRenderer mr;
    bool isSelected = false;
    bool playmode, glow;
    private Rigidbody rbPlayer;
    public float movementSpeed = 1f;
    private Vector3 moveDirection = Vector3.zero;
    string currentTeam;

    public GameObject unitCam, mapCam;

    //Gets the mesh of the spawners so they can be changed.
    private void Awake()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
        rbPlayer = gameObject.GetComponent<Rigidbody>();
        mapCam = Camera.main.gameObject;
    }

    void GetValues()
    {
        playmode = Gamemanager.Instance.unitConfig;
        currentTeam = Gamemanager.Instance.team[Gamemanager.Instance.teamSelected];
    }

    private void OnMouseEnter()
    {
        GetValues();

        if (gameObject.tag == currentTeam && !playmode)
        {
            Debug.Log(currentTeam);
            glow = true;
            Glowing();
        }
    }

    private void OnMouseExit()
    {
        GetValues();

        if (gameObject.tag == currentTeam && !playmode)
        {
            Debug.Log(currentTeam);
            glow = false;
            Glowing();
        }
    }

    private void OnMouseDown()
    {
        //When an unit is selected check if the right team is playing.
        //If it is not the right team then return an value.
        if (gameObject.tag == currentTeam && !playmode)
        {
            StartCoroutine(SwitchAni());
        }
    }

    IEnumerator SwitchAni()
    {
        //Play animation
        isSelected = true;
        yield return null;
        unitCam.SetActive(true);
        mapCam.SetActive(false);
    }

    private void Glowing()
    {
        switch (glow)
        {
            case true:
                mr.material.EnableKeyword("_EMISSION");
                break;
            case false:
                mr.material.DisableKeyword("_EMISSION");
                break;
        }
    }

    private void FixedUpdate()
    {
        if (isSelected && !playmode)
        {
            //Handles the walking.
            //sets the moveDirection to the input that has been given by the player.
            //GetAxisRaw = doesn't add the force up.
            moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            //Moves the rigidbody.
            rbPlayer.transform.Translate(moveDirection * Time.deltaTime * movementSpeed);
            rbPlayer.transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        }
    }
}
