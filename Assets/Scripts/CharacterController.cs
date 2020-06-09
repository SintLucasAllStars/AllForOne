using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public GameObject TargetUnit;

    Camera OverviewCam;

    private void Awake()
    {
        OverviewCam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            if (Physics.Raycast(OverviewCam.ScreenPointToRay(Input.mousePosition).origin,
                OverviewCam.ScreenPointToRay(Input.mousePosition).direction, out RaycastHit hit, 100,
                Physics.DefaultRaycastLayers))
            {
                TargetUnit = hit.transform.gameObject;
                Debug.Log(hit.transform.name);
            }
        }
    }

}
