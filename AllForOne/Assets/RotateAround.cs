using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public Transform map;
    public float turnspeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.RotateAround(map.position, Vector3.up, turnspeed * horizontal * Time.deltaTime);
    }
}
