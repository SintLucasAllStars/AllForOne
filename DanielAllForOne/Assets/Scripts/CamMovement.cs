using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{

    public bool IsCamAvailable = false;

    public IEnumerator MoveCamera()
    {
        IsCamAvailable = true;

        while (IsCamAvailable)
        {
            Vector3 newPosition = new Vector3();

            if (Input.GetKey(KeyCode.W))
                newPosition += Vector3.forward;
            if (Input.GetKey(KeyCode.S))
                newPosition += -Vector3.forward;
            if (Input.GetKey(KeyCode.A))
                newPosition += -Vector3.right;
            if (Input.GetKey(KeyCode.D))
                newPosition += Vector3.right;

            transform.position += newPosition;

            yield return null;
        }
    }
}
