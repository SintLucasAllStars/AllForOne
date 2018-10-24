using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private RawImage _crossHair;
    [SerializeField] private float _maxAngle;
    [SerializeField] private float _minAngle;        

    private float _lookHeight;
    [SerializeField] private float _distance = 5f;

    [SerializeField] private Vector2 _yClamp;

    public float mouseSpeed;


  
    public Vector3 GetCrossHairPosition()
    {
        return _crossHair.transform.position;
    }
    
	// Use this for initialization
	void Start () {
		
	}
	

}
