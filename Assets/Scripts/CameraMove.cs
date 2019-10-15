using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public int scrollSpeed;

    void Update()
    {
        float xAxisValue = Input.GetAxis("Horizontal");
        float zAxisValue = Input.GetAxis("Vertical");
        float scrollAxisValue = Input.GetAxis("Mouse ScrollWheel");

        if (Camera.main != null)
        {
            Camera.main.transform.Translate(new Vector3(xAxisValue, zAxisValue, 0.0f));
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            Debug.Log("scroll");
            Camera.main.transform.Translate(new Vector3(0.0f, 0.0f, scrollAxisValue + scrollSpeed));
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            Debug.Log("scroll");
            Camera.main.transform.Translate(new Vector3(0.0f, 0.0f, +scrollAxisValue + -scrollSpeed));
        }
    }
}
