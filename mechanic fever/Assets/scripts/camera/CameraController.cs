using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float zoomSpeed;
    public float rotationPerPress;
    public float rotationSpeed;

    private Transform mainCamera;
    private Quaternion targetRotation;

    private void Start()
    {
        mainCamera = transform.GetChild(0);
        targetRotation = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CameraMovement();
        CameraRotation();
    }

    private void CameraMovement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");


        transform.position += (hDirection * speed * Time.deltaTime) * transform.right + transform.forward * (vDirection * speed * Time.deltaTime);
    }

    private void CameraRotation()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRotation *= Quaternion.Euler(0, rotationPerPress, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            targetRotation *= Quaternion.Euler(0, -rotationPerPress, 0);
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    //TODO: add camera Zoom
}
