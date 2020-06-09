using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal") * Time.deltaTime * cameraSpeed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * cameraSpeed;

        transform.Translate(x, 0, z, Space.World);
    }
}
