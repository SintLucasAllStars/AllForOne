using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Player : MonoBehaviour
{

    //Camera
    public float mouseSens = 10;
    public Transform target;
    public float DistanceFormTarget = 2; 
    float x, y;

    enum PlayerState {None, Selected, Controlling }; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ThirdPersonCamera()
    {
        x += Input.GetAxisRaw("Mouse X") * mouseSens;
        y += Input.GetAxisRaw("Mouse Y") * mouseSens;
        y = Mathf.Clamp(y, -40, 85);

        Vector3 targetRot = new Vector3(y, x);
        transform.eulerAngles = targetRot;

        transform.position = target.position - transform.position * DistanceFormTarget; 


    }
}
