using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    public int scrollSpeed;

    public GameObject objectToFollow;

    public float speed = 2.0f;

    public void Start()
    {

    }

    void Update()
    {
        /*float xAxisValue = Input.GetAxis("Horizontal");
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
        }*/

        //CAMERA SMOTHE OBJECT
        float interpolation = speed * Time.deltaTime;

        Vector3 position = this.transform.position;
        Quaternion rotation = this.transform.rotation;
        position.y = Mathf.Lerp(this.transform.position.y, objectToFollow.transform.position.y, interpolation);
        position.x = Mathf.Lerp(this.transform.position.x, objectToFollow.transform.position.x, interpolation);
        position.z = Mathf.Lerp(this.transform.position.z, objectToFollow.transform.position.z, interpolation);
        rotation.x = Mathf.Lerp(this.transform.rotation.x, objectToFollow.transform.rotation.x, interpolation);
        rotation.y = Mathf.Lerp(this.transform.rotation.y, objectToFollow.transform.rotation.y, interpolation);
        rotation.z = Mathf.Lerp(this.transform.rotation.z, objectToFollow.transform.rotation.z, interpolation);

        this.transform.position = position;
        this.transform.rotation = rotation;
    }

    public void CameraParent()
    {
        Camera.main.transform.parent = objectToFollow.transform;
    }
}
