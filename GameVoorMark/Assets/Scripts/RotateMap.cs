using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateMap : MonoBehaviour
{
    public float rotateSpeed;

    public Material skybox;

    private void Update()
    {
        skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
