using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public int mouseBorderMove = -1;
    public int cameraSpeed = 10;

    private Camera cam;

    private void Awake()
    {
        if (GetComponent<Camera>() != null)
        {
            cam = GetComponent<Camera>();
        }
        else
        {
            Debug.LogWarning("The CameraController.cs in gameobject \"" + name + "\" has no camera component. Destroy this script");
            Destroy(this);
        }
    }

    private void Start()
    {
        if (mouseBorderMove < 0)
            mouseBorderMove = (Screen.width + Screen.height / 2) / 8;
    }


    void Update ()
    {
        transform.Translate(InputController() * cameraSpeed * Time.deltaTime);
    }

    private Vector3 InputController ()
    {
        Vector3 inputPos = new Vector3(0, 0, 0);

        inputPos.x += Input.GetAxis("Horizontal");
        inputPos.y += Input.GetAxis("Vertical");
        inputPos.z += Input.GetAxis("Mouse ScrollWheel") * 10;

        //Mouse to left
        if (Input.mousePosition.x < mouseBorderMove)
            inputPos.x += 1f / mouseBorderMove * (Input.mousePosition.x - mouseBorderMove);

        //Mouse to right
        if (Input.mousePosition.x > Screen.width - mouseBorderMove)
            inputPos.x -= 1f / mouseBorderMove * ((Screen.width - Input.mousePosition.x) - mouseBorderMove);

        //Mouse to up
        if (Input.mousePosition.y > Screen.height - mouseBorderMove)
            inputPos.y -= 1f / mouseBorderMove * ((Screen.height - Input.mousePosition.y) - mouseBorderMove);

        //Move to down
        if (Input.mousePosition.y < mouseBorderMove)
            inputPos.y += 1f / mouseBorderMove * (Input.mousePosition.y - mouseBorderMove);

        return inputPos;
    }
}
