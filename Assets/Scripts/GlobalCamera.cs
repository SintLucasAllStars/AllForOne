using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalCamera : MonoBehaviour {

    public static GlobalCamera instance = null;

    public Camera Camera { get { return camera; } }

    [Header("ViewTop")]
    [SerializeField] private Vector2 offsetTopX  = new Vector2(10,10);
    [SerializeField] private Vector2 offsetTopY = new Vector2(10, 10);
    [SerializeField] private float speedTop     = 1;
    [SerializeField] private LayerMask cullingMaskTop = ~0;

    [Header("View3th")]
    [SerializeField] private Vector3 offset3th = new Vector3(0, 1.8f, 0);
    [SerializeField] private float distance = 2.5f;
    [SerializeField] private LayerMask cullingMask3th = 0;

    private Unit unit;
    private Vector2 input;

    private bool isUnit = false;
    private bool isUnitCamPos = false;

    private new Camera camera;

    private void Awake()
    {
        instance = this;
        camera = GetComponent<Camera>();
    }

    public void SetUnitCamera(Unit unit)
    {
        isUnit = true;
        this.unit = unit;
        camera.cullingMask = cullingMask3th;
    }

    public void SetPlayerCamera()
    {
        isUnit = false;
        transform.position = new Vector3(0, 10, 0);
        transform.rotation = Quaternion.Euler(60, 0, 0);
        camera.cullingMask = cullingMaskTop;
    }

    private void Start()
    {
        SetPlayerCamera();
    }

    private void LateUpdate()
    {
        if(isUnit)
        {
            input.y += Input.GetAxis("Mouse X");
            input.x = Mathf.Clamp(input.x += Input.GetAxis("Mouse Y"), -20, 30);

            Vector3 direction = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(input.x, input.y, 0);
            transform.position = unit.transform.position + offset3th + rotation * direction;
            transform.LookAt(unit.transform.position + offset3th);
        }
        else
        {
            float motionX = Input.GetAxis("Horizontal") * Time.deltaTime * 10 * speedTop;
            float motionY = Input.GetAxis("Vertical") * Time.deltaTime * 10 * speedTop;

            //Clamp camera position within set distance from the player.
            if (motionX > 0 && transform.position.x > offsetTopX.y)
                motionX = 0;

            if (motionX < 0 && transform.position.x < -offsetTopX.x)
                motionX = 0;

            if (motionY > 0 && transform.position.z > offsetTopY.y)
                motionY = 0;

            if (motionY < 0 && transform.position.z < -offsetTopY.x)
                motionY = 0;

            transform.Translate(motionX, 0, motionY, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.U))
            ResetPosition();
    }

    private void ResetPosition()
    {
        input.x = 0;
        input.y = 0;
    }
}
