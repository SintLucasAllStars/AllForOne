using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    float boost = 2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.CompareTag("Player1") || other.transform.gameObject.CompareTag("Player2"))
        {
            Character p = other.GetComponent<Character>();
            StartCoroutine(p.SpeedUp());
            Destroy(this.gameObject);
        }
    }
}
