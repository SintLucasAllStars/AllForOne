using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRotate : MonoBehaviour
{
    public int powerUpID = 0;
    private float rotationSpeed = 90f;

    private void Update()
    {
        switch(powerUpID)
        {
            case 1:
                transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
                break;

            case 2:
                transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
                break;
        }
        
    }
}
