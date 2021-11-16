using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotation : MonoBehaviour
{
    public Material skybox;

    public float rotateSpeed;

    private void Update()
    {
        skybox.SetFloat("_Rotation", Time.time * rotateSpeed);
    }
}
