using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float m_Speed;
    // Update is called once per frame
    void Update()
    {
		transform.eulerAngles += new Vector3(0, m_Speed, 0);       
    }
}
