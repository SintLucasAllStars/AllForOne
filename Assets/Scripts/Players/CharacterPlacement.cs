using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Players;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
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


    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private RaycastHit _raycastHit;
    private MaterialPropertyBlock [] _materialPropertyBlocks;
    [SerializeField] private SkinnedMeshRenderer [] _skinnedMeshRenderers;



    private void Awake()
    {
        _thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        _userControl = GetComponent<ThirdPersonUserControl>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        
;            
        _materialPropertyBlocks = new MaterialPropertyBlock[_skinnedMeshRenderers.Length];
        for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
        {
            _materialPropertyBlocks[i] = new MaterialPropertyBlock();
            _skinnedMeshRenderers[i].SetPropertyBlock(_materialPropertyBlocks[i]);
            _materialPropertyBlocks[i].SetColor("_Color", Color.white);
            _skinnedMeshRenderers[i].SetPropertyBlock(_materialPropertyBlocks[i]);
        }




    }


    private void Update()
    {

        Debug.Log(OnFloor());

        if (!Input.GetMouseButtonDown(0))
        {
            FollowMouse();
            SetColor(OnFloor());
        }
        else if(OnFloor())
        {
            PlaceCharacter();
            for (int i = 0; i < _skinnedMeshRenderers.Length; i++)
            {
                _materialPropertyBlocks[i].SetColor("_Color", Color.white);
                _skinnedMeshRenderers[i].SetPropertyBlock(_materialPropertyBlocks[i]);
            }

        }


    }


    private void SetColor(bool onFloor)
    {

        for (int i = 0; i < _skinnedMeshRenderers.Length ; i++)
        {
            _skinnedMeshRenderers[i].GetPropertyBlock(_materialPropertyBlocks[i]);        
            if (!onFloor)
            {
                if (_materialPropertyBlocks[i].GetColor("_Color") != Color.white)
                {
                    Color lerpColor = Color.Lerp(Color.black, Color.white,3f *Time.deltaTime);
                    _materialPropertyBlocks[i].SetColor("_Color", lerpColor);

                }
            }
            else
            {
                if (_materialPropertyBlocks[i].GetColor("_Color") != Color.black)
                {
                    Color lerpColor = Color.Lerp(Color.white, Color.black, 3f * Time.deltaTime);
                    _materialPropertyBlocks[i].SetColor("_Color", lerpColor);
                }
            }
            _skinnedMeshRenderers[i].SetPropertyBlock(_materialPropertyBlocks[i]);
        }
 
    }

    private void PlaceCharacter()
    {
        
        _thirdPersonCharacter.enabled = true;
        _userControl.enabled = true;
        _rigidbody.useGravity = true;
        GameManager.Instance.CharacterPlaced();
        enabled = false;

    }


    private bool OnFloor()
    {
        Ray myRay = new Ray(transform.position, -transform.up);
        if (!Physics.Raycast(myRay, out _raycastHit, 50f)) return false;
        return !_raycastHit.collider.CompareTag("Terrain");
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
