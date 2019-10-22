using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    float heal = 5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.CompareTag("Player1") || other.transform.gameObject.CompareTag("Player2"))
        {
            Character p = other.GetComponent<Character>();
            p.Heal(heal);
            Destroy(this.gameObject);
        }
    }
}
