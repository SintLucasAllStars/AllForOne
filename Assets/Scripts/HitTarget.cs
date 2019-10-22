using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitTarget : MonoBehaviour
{
    public float health;

    public void Start()
    {
        health = GameInfo.Health;
    }
    
    public void TakeDamage(float amount)
    {
        health -= amount/2;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
