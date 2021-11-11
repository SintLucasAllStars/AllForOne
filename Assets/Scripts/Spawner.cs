using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private MeshRenderer mr;
    private bool glow, placedUnit;
    public static bool overlay;
    private Transform _transform;

    private string spawnerTag = "Spawner";

    //Gets the mesh of the spawners so they can be changed.
    private void Awake()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    //Only activates the glow effect if the overlay is off.
    private void OnMouseEnter()
    {
        if (gameObject.tag == spawnerTag && !overlay)
        {
            glow = true;
            Glowing();       
        }
    }

    //Only de-activates the glow effect if the overlay is off.
    private void OnMouseExit()
    {
        if (gameObject.tag == spawnerTag && !overlay)
        {
            glow = false;
            Glowing();
        }
    }

    //Checks if the cursor is on an spawner object.
    //Spawns the object (in gamemanager script).
    private void OnMouseDown()
    {
        if (gameObject.tag == spawnerTag && !placedUnit && !overlay)
        {
            _transform = gameObject.transform;
            Gamemanager.Instance.Spawn(_transform);
            overlay = true;
            placedUnit = true;
            StartCoroutine(ResetSpawner());
        }
    }

    //Resets the UI.
    //If player spawned an unit, the Unit selector UI will turn-on.
    private IEnumerator ResetSpawner()
    {
        Gamemanager.Instance.TeamManager();
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.SwitchUnitSUI();
        placedUnit = false;
    }

    //Handles the glow effect on the spawn cubes.
    //Glows when the mouse hovers over the object, if not, no glow.
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
}
