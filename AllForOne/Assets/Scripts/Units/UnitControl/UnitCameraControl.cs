using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCameraControl : MonoBehaviour
{
    public Unit unit;

    private float currentY = 0.0f;
    private float sensivityY = 4f;

    private void Update()
    {
        currentY = Input.GetAxis("Mouse X") * sensivityY;
    }

    private void FixedUpdate()
    {
        if ((unit.isSelected) && (!unit.inCombat))
        {
            transform.RotateAround(transform.position, -Vector3.up, currentY);
        }
    }

    public void LookAtEnemy(Transform target)
    {
        transform.LookAt(target);
        transform.localRotation = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
    }
}