using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public float zoomSpeed;
    public float rotationPerPress;
    public float rotationSpeed;

    private Vector2 boundries;

    public Transform mainCamera;
    private Quaternion targetRotation;

    private void Start()
    {
        Init();
        GameManager.gameManager.OnReset.AddListener(Init);
    }

    private void Init()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);

        boundries = GameManager.gameManager.fieldSize;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.gameManager.controllingCamera)
        {
            CameraMovement();
            CameraRotation();
        }
    }

    private void CameraMovement()
    {
        float hDirection = Input.GetAxis("Horizontal");
        float vDirection = Input.GetAxis("Vertical");

        transform.position += (hDirection * speed * Time.deltaTime) * transform.right + transform.forward * (vDirection * speed * Time.deltaTime);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -boundries.x, boundries.x), 0, Mathf.Clamp(transform.position.z, -boundries.y, boundries.y));
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
