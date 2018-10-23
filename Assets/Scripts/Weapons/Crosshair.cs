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


    public void LookHeight(float value)
    {
        _lookHeight += value;
        if (_lookHeight > _maxAngle || _lookHeight < _minAngle)
        {
            _lookHeight -= value;
        }
    }
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float clampValue = Mathf.Clamp(Input.GetAxis("Mouse Y"), _yClamp.x, _yClamp.y);
	    _crossHair.transform.position = new Vector3(transform.position.x, transform.position.y + clampValue);

	}
}
