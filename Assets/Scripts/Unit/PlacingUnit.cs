using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingUnit : MonoBehaviour
{
    private LayerMask placingArea;

    // Start is called before the first frame update
    void Start()
    {
        string[] layers = {"PlacingArea"};
        placingArea = LayerMask.GetMask(layers);
    }

    // Update is called once per frame
    void Update()
    {
        if (!StoreManager.instance.storePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                RaycastHit hit;
                
                Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.green, 2f);

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, placingArea))
                {
                    StoreManager.instance.CreateUnit(hit.point);
                    //Debug.Log($"Did Hit {hit.collider.gameObject.name}");
                }
                else
                {
                    Debug.Log("Did not Hit");
                }
            }
        }
    }
}
