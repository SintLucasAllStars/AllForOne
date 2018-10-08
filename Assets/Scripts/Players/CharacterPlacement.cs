using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class CharacterPlacement : MonoBehaviour
{

    private Vector3 _mousePosition;
    
    [Header("CLAMP VALUES")]
    [SerializeField] private Vector2 _xClamp;
    [SerializeField] private Vector2 _yClamp;
    [SerializeField] private Vector2 _zClamp;



    private Vector3 _screenPoint;
    private ThirdPersonController _thirdPersonController ;
    
    
    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
    }


    private void Update()
    {
      
        if (!Input.GetMouseButtonDown(0))
        {
            FollowMouse();
        }
        else
        {
            _thirdPersonController.enabled = true;
            enabled = false;
        }
        
    }

    private void FollowMouse()
    {
        _screenPoint = Camera.main.WorldToScreenPoint(transform.position);        
        Vector3 currentScreenPoint = new Vector3
        (
            Input.mousePosition.x,
            Input.mousePosition.y,
            _screenPoint.z
        );

        // ReSharper disable once Unity.InefficientCameraMainUsage
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint);
        
        _mousePosition = curPosition;
        _mousePosition.x = Mathf.Clamp(_mousePosition.x, _xClamp.x, _xClamp.y);
        _mousePosition.y = Mathf.Clamp(_mousePosition.y, _yClamp.x, _yClamp.y);
        _mousePosition.z = Mathf.Clamp(_mousePosition.z, _zClamp.x, _zClamp.y);
        Debug.Log(_mousePosition);
        transform.position = _mousePosition;
    }

}
