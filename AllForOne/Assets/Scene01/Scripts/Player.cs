using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    private Unit unit; 

    //Input
    Vector2 movementInput;

    //Third Person Camera
    [Header("Camera Variables")]
    [SerializeField]
    public Transform target;

    public Camera mainCamera;
    public float mouseSens = 10;
    public float distanceFromTarget = 2;
    public float rotSmoothDuration = .12f;
    public Vector2 pitchMinMax = new Vector2(-40, 85);

    //Top Down Camera
    [Header("Top Down Camera Variables")]
    public float cameraSpeed; 

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
        ControlState();
        Click();
    }

    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.Log("click");
            curState = currentState.None;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<Unit>() != null)
                {
                    Debug.Log(hit.collider.name);
                    target = hit.collider.transform;
                    curState = currentState.Selected;
                }
            }
        }

    }

    void ControlState()
    {
        switch (curState)
        {
            case currentState.None:
                yaw = 0;
                pitch = 0;
                target = null;
                TopDownCamera();
                break;
            case currentState.Selected:
                ThirdPersonCamera();
                break;
            case currentState.Controlling:
                break;
            default:
                break;
        }
    }

    void TopDownCamera()
    {
        float horizontal = Input.GetAxis("Horizontal") * cameraSpeed;
        float vertical = Input.GetAxis("Vertical") * cameraSpeed;
        
        Vector3 TopDownTransform = new Vector3(horizontal, 0, vertical);
        TopDownTransform += new Vector3(mainCamera.transform.position.x, 20, mainCamera.transform.position.z);

        Vector3 TopDownRotation = new Vector3(60, 0, 0);

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, TopDownTransform, Time.deltaTime * cameraSpeed);
        mainCamera.transform.eulerAngles = new Vector3(55, 0, 0);
        //mainCamera.transform.eulerAngles = Vector3.Lerp(mainCamera.transform.eulerAngles, TopDownRotation, Time.deltaTime * cameraSpeed);
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
