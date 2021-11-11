using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Spawner : MonoBehaviour
{
    private bool placedUnit;
    public static bool overlay;

    //Checks if the cursor is on an spawner object.
    //Spawns the object (in gamemanager script).
    private void OnMouseDown()
    {
        float length = 10f;
        LayerMask mask;
        Vector3 hitpos;

        mask = LayerMask.GetMask("Spawner");

        Camera cam = Gamemanager.Instance.houseCam[Gamemanager.Instance.activeCam];
        print("test");

        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject() && !placedUnit && !overlay)
        {
            if (Physics.Raycast(ray, out hit, length, mask))
            {
                hitpos = hit.point;
                Gamemanager.Instance.Spawn(hitpos);
                overlay = true;
                placedUnit = true;
                StartCoroutine(ResetSpawner());
            }
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
}
