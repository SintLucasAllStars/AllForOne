using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private MeshRenderer mr;
    private bool glow, placedUnit;
    public static bool overlay;

    private string spawnerTag = "Spawner";

    private Transform _transform;

    private void Awake()
    {
        mr = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        if (gameObject.tag == spawnerTag && !overlay)
        {
            glow = true;
            Glowing();         
        }
    }

    private void OnMouseExit()
    {
        if (gameObject.tag == spawnerTag && !overlay)
        {
            glow = false;
            Glowing();
        }
    }

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

    private IEnumerator ResetSpawner()
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.SwitchUnitSUI();
        placedUnit = false;
        print("TEST");
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
}
