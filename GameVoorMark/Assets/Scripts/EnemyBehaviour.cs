using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float enemyMovement;

    private void Update()
    {
        transform.Translate(enemyMovement * Time.deltaTime, 0, 0);
    }
}
