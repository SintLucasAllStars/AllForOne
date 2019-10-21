using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMovements : MonoBehaviour
{
    public bool rotateY;
    public bool rotateZ;

    void Update()
    {
        if(rotateZ)
            transform.Rotate(new Vector3(0, 0, 20 * Time.deltaTime));
        else if(rotateY)
            transform.Rotate(new Vector3(0, 20 * Time.deltaTime, 0));
    }
}
