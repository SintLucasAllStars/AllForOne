using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Unit testUnit;

    // Start is called before the first frame update
    void Start()
    {
        testUnit = new Unit(1, "Name here", 100, 0, 0, 0);
        //Debug.Log(testUnit.getHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
