using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    MeshRenderer mr;
    bool isSelected = false;
    bool playmode, glow;

    string currentTeam;

    //Gets the mesh of the spawners so they can be changed.
    private void Awake()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
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

    void SelectUnit()
    {
        //When an unit is selected check if the right team is playing.
        //If it is not the right team then return an value.
    }

    void EnableWalking()
    {
        //This will enables the walking/movement for the unit if it passses the check.
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

    private void Update()
    {
        //Handles the walking.
    }
}
