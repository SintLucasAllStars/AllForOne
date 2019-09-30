using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{

    private Warrior warrior;

    public void Start()
    {
        warrior = new Warrior();
    }


}
