using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public CinemachineVirtualCamera top;
    public CinemachineFreeLook perspective;

    private bool TopDown = true;
    
    public bool SwitchCamera()
    {
        if (TopDown)
        {
            top.Priority = 0;
            perspective.Priority = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            TopDown = !TopDown;
            return true;
        }
        
        perspective.Priority = 0;
        top.Priority = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        TopDown = !TopDown;
        return true;
    }
}
