using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformRotation : MonoBehaviour
{
    [SerializeField] Vector3 RotationTo;

    private void OnEnable()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.rotation = Quaternion.Euler(RotationTo);
        }
    }
}
