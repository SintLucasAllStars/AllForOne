using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{

    private float scrollSpeed;

    public void Start()
    {
        scrollSpeed = 10;
    }

    private void FixedUpdate()
    {
        Move();
    }       

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float zoom = Input.GetAxis("Mouse ScrollWheel");

        transform.Translate(horizontal, vertical, zoom * scrollSpeed);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, 4, 14), Mathf.Clamp(transform.position.y, 6, 15), Mathf.Clamp(transform.position.z, 6, 16));
    }
}
