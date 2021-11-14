using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControler : MonoBehaviour {
    /*
        EXTENDED FLYCAM
            Desi Quintans (CowfaceGames.com), 17 August 2012.
            Based on FlyThrough.js by Slin (http://wiki.unity3d.com/index.php/FlyThrough), 17 May 2011.
     
        LICENSE
            Free as in speech, and free as in beer.
     
        FEATURES
            WASD/Arrows:    Movement
                      Q:    Climb
                      E:    Drop
                          Shift:    Move faster
                        Control:    Move slower
                            End:    Toggle cursor locking to screen (you can also press Ctrl+P to toggle play mode on and off).
        */

    public float cameraSensitivity = 90;
    public float normalMoveSpeed = 10;
    public float slowMoveFactor = 0.25f;
    public float fastMoveFactor = 3;
    public float rotationOffsetY = 0;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        rotationX += Input.GetAxis("Mouse X") * cameraSensitivity * Time.deltaTime;

        rotationY = Mathf.Clamp(rotationY, -90, 90);

        // Anton: added 90 degrees to prevent 90 degrees rotation counter clock wise
        transform.rotation = Quaternion.AngleAxis(rotationX + rotationOffsetY, Vector3.up);
        transform.rotation *= Quaternion.AngleAxis(rotationY, Vector3.left);

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
            transform.position += transform.forward * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Vertical") *
                                  Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * fastMoveFactor) * Input.GetAxis("Horizontal") *
                                  Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) {
            transform.position += transform.forward * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Vertical") *
                                  Time.deltaTime;
            transform.position += transform.right * (normalMoveSpeed * slowMoveFactor) * Input.GetAxis("Horizontal") *
                                  Time.deltaTime;
        }
        else {
            transform.position += transform.forward * normalMoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
            transform.position += transform.right * normalMoveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        }
    }
}