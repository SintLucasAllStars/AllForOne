using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<GameObject> Powerups;
    public GameObject bootsprite;

    private GameObject selected;
    private float select;

    void Update()
    {
        //if not pressing v scroll trough inventory 
        //(have to do it like this to stop it from scrolling and moving camera at once)
        if (!Input.GetKey(KeyCode.V)) { select += Input.mouseScrollDelta.y;}
        if (select > Powerups.Count - 1) select -= Powerups.Count;
        if (select < 0f) select += Powerups.Count;
    
        selected = Powerups[Mathf.FloorToInt(select)];

        foreach (GameObject o in Powerups)
        {
            if (o == selected) o.SetActive(true);
            else o.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Boots"))
        {
            Debug.Log("triggerd");
            Powerups.Add(bootsprite);
            col.gameObject.SetActive(false);
        }
    }
}
