using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    private Unit unit; 

    //Input
    Vector2 movementInput;

    //Camera
    [Header("Camera Variables")]
    [SerializeField]
    public Transform target;

    public Camera mainCamera;
    public float mouseSens = 10;
    public float distanceFromTarget = 2;
    public float rotSmoothDuration = .12f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    float pitch, yaw; 
    Vector3 rotSmoothing;
    Vector3 curRotation; 
    

    enum currentState {None, Selected, Controlling };
    private currentState curState; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Click();
        ThirdPersonCamera();
    }

    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.Log("click");
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Unit>() != null)
                {
                    Debug.Log(hit.collider.name);
                    target = hit.collider.transform;
                }
            }
        }

    }

    void SwitchCase()
    {
        switch (curState)
        {
            case currentState.None:

                break;
            case currentState.Selected:

                break;
            case currentState.Controlling:
                break;
            default:
                break;
        }
    }

    void ThirdPersonCamera()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSens;
        pitch -= Input.GetAxis("Mouse Y") * mouseSens;
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        curRotation = Vector3.SmoothDamp(curRotation, new Vector3(pitch, yaw), ref rotSmoothing, rotSmoothDuration);
        mainCamera.transform.eulerAngles = curRotation; 
        mainCamera.transform.position = target.transform.position - (mainCamera.transform.forward * distanceFromTarget); 


    }
}
