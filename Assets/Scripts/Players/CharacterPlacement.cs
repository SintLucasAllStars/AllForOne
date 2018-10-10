using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterPlacement : MonoBehaviour
{

    private Vector3 _mousePosition;
    
    [Header("CLAMP VALUES")]
    [SerializeField] private Vector2 _xClamp;
    [SerializeField] private Vector2 _yClamp;
    [SerializeField] private Vector2 _zClamp;



    private Vector3 _screenPoint;
    private ThirdPersonCharacter _thirdPersonCharacter;
    private ThirdPersonUserControl _userControl;
    private Rigidbody _rigidbody;
    
    
    private void Awake()
    {
        _thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        _userControl = GetComponent<ThirdPersonUserControl>();
        _rigidbody = GetComponent<Rigidbody>();
    }


    private void Update()
    {
      
        if (!Input.GetMouseButtonDown(0))
        {
            FollowMouse();
        }
        else
        {
            _thirdPersonCharacter.enabled = true;
            _userControl.enabled = true;
            _rigidbody.useGravity = true;
            GameManager.Instance.CharacterPlaced();
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
        transform.position = _mousePosition;
    }

}
