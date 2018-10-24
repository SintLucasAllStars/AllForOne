using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingCamera : MonoBehaviour
{
    static Camera[] _cameras = null;

    void Start()
    {
        if (_cameras == null)
            _cameras = GameObject.FindObjectsOfType<Camera>();
    }

    void OnMouseDown()
    {
        foreach (Camera cam in _cameras)
            cam.enabled = !cam.enabled;
    }
}
