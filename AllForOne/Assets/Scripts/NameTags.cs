using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameTags : MonoBehaviour
{
    private Camera _camera;

    private void Update()
    {
        if (_camera != null && _camera.enabled)
        {
            transform.LookAt(_camera.transform);
            return;
        }
        for (int i = 0; i < Camera.allCamerasCount; i++)
        {
            if (Camera.allCameras[i].enabled)
            {
                _camera = Camera.allCameras[i];
                return;
            }
        }
    }
}
