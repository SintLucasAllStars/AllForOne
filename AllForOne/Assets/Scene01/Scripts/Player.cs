using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI; 
using UnityEngine;

public class Player : MonoBehaviour
{ 
    private Unit unit;
    public Transform CameraPosition; 

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

    //Third Person Movement
    public float movementSpeed;

    //Top Down Camera
    [Header("Top Down Camera Variables")]
    public float cameraSpeed;

    //Variables 
    float pitch, yaw; 
    Vector3 rotSmoothing;
    Vector3 curRotation;
    float startingTime = 10.0f;
    float timeLeft = 0.0f; 

    enum currentState {None, Selected, Controlling };
    private currentState curState;


    Unit curUnit; 

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = startingTime; 
    }

    // Update is called once per frame
    void Update()
    {
        ControlState();
        Click(); 
    }

    void Click()
    {
        if (Input.GetMouseButtonDown(0) && curState != currentState.Controlling)
        {
            RaycastHit hit;
            Debug.Log("click");
            curState = currentState.None;
            curUnit = null; 
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.name);
                curUnit = hit.collider.GetComponent<Unit>();

               if (curUnit != null)
               {
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
                Debug.Log(curState);
                break;
            case currentState.Selected:
                ThirdPersonCamera();
                if (Input.GetKeyDown(KeyCode.Space) == true)
                {
                    curState = currentState.Controlling; 
                }
                Debug.Log(curState);
                break;
            case currentState.Controlling:
                ThirdPersonCamera();
                curUnit.Move(mainCamera);
                timeLeft -= 1.0f * Time.deltaTime;
                Debug.Log(timeLeft);
                if (timeLeft < 0)
                {
                    curState = currentState.None;
                    timeLeft = 10.0f; 
                }
                
                break;
            default:
                break;
        }
    }

    //Camera's 
    void TopDownCamera()
    {
        Vector2 _input = new Vector2(Input.GetAxisRaw("Horizontal") * cameraSpeed,Input.GetAxisRaw("Vertical") * cameraSpeed);

        Vector3 TopDownTransform = new Vector3(_input.x, 0, _input.y);
        TopDownTransform += new Vector3(mainCamera.transform.position.x, 20, mainCamera.transform.position.z);

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, TopDownTransform, Time.deltaTime * cameraSpeed);
        mainCamera.transform.rotation = Quaternion.Lerp(mainCamera.transform.rotation, CameraPosition.rotation, Time.deltaTime * cameraSpeed);
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
