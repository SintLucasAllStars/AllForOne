using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 8 * Time.deltaTime, 0);
    }

    public void Disable()
    {
        enabled = false;
        transform.rotation = Quaternion.identity;
    }
}