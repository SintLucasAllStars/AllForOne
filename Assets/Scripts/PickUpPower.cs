using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPower : MonoBehaviour
{
    public List<PowerUp> powers = new List<PowerUp>();

    public PowerUp PickUp()
    {
        int rand = Random.Range(0, powers.Count);

        Destroy(gameObject, 0.1f);
        return powers[rand];
    }
}
