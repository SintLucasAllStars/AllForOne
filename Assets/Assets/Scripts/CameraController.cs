using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal.Input;

public class CameraController : MonoBehaviour
{

    public float ScreenOffset;

    public float cameraSpeed;

    public Camera TopDownCamera;

    public Camera ThirdPersonCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    Vector2 mousePos = Input.mousePosition;
        
	    if (mousePos.y >= Screen.height - ScreenOffset )
	    {
            transform.position += transform.up * cameraSpeed * Time.deltaTime;
        }
        if (mousePos.x >= Screen.width - ScreenOffset)
        {
            transform.position += transform.right * cameraSpeed * Time.deltaTime;
        }
	    if (mousePos.y <= Screen.height - Screen.height +ScreenOffset)
	    {
	        transform.position -= transform.up * cameraSpeed * Time.deltaTime;
	    }
	    if (mousePos.x <= Screen.width -Screen.width + ScreenOffset)
	    {
	        transform.position -= transform.right * cameraSpeed * Time.deltaTime;
	    }
    }
}
