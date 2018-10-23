using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int mouseBorderMove = -1;
    public int cameraSpeed = 10;
    public int cameraHeight = 15;

    public enum CameraControlEnum { none, playerBlueView, playerRedView, mapControl, playerThirdPerson }
    public CameraControlEnum CameraCurrentControl;
    private Camera cam;

    public GameObject playerTarget;
    public bool cameraAfterControl = false;

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


    void LateUpdate ()
    {
        switch(CameraCurrentControl)
        {
            case CameraControlEnum.mapControl:
                transform.Translate(InputController() * cameraSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(CameraAngleTopDown());
                break;

            case CameraControlEnum.playerBlueView:
                Vector3 targetB = PlayerManager.instance.playerBlue.playerHome.position;
                targetB.y = cameraHeight;
                transform.position = Vector3.MoveTowards(transform.position, targetB, Vector3.Distance(transform.position, targetB) * 2f * Time.deltaTime);

                if (isCameraTopDown())
                {
                    if (cameraAfterControl)
                        CameraCurrentControl = CameraControlEnum.mapControl;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(CameraAngleTopDown());
                }
                break;
            case CameraControlEnum.playerRedView:
                Vector3 targetR = PlayerManager.instance.playerRed.playerHome.position;
                targetR.y = cameraHeight;
                transform.position = Vector3.MoveTowards(transform.position, targetR, Vector3.Distance(transform.position, targetR) * 2f * Time.deltaTime);

                if (isCameraTopDown())
                {
                    if (cameraAfterControl)
                        CameraCurrentControl = CameraControlEnum.mapControl;
                }
                else
                {
                    transform.rotation = Quaternion.Euler(CameraAngleTopDown());
                }
                break;

            case CameraControlEnum.playerThirdPerson:
                float currentAngle = transform.eulerAngles.y;
                float nearAngle = playerTarget.transform.localEulerAngles.y;

                float distanceAngle = nearAngle > currentAngle ? nearAngle - currentAngle : currentAngle - nearAngle;

                float angle = Mathf.MoveTowardsAngle(currentAngle, nearAngle, Time.deltaTime * 5f * distanceAngle);
                Quaternion rotation = Quaternion.Euler(0, angle, 0);

                transform.position = playerTarget.transform.position - (rotation * new Vector3(0, -2, 2.3f));
                transform.LookAt(playerTarget.transform);
                transform.Rotate(-30, 0, 0);
                break;
        }
    }


    private bool isCameraTopDown ()
    {
        return (transform.eulerAngles.x == 90 &&
            transform.eulerAngles.y == 0 &&
            transform.eulerAngles.z == 0);
    }

    private Vector3 CameraAngleTopDown ()
    {
        Vector3 targetAngle = new Vector3(90, transform.eulerAngles.y > 180 ? 360 : 0, 0);
        return Vector3.MoveTowards(transform.eulerAngles, targetAngle, Time.deltaTime * 60f);
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
