﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Players;
using Players.Animation;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

public class CharacterMono : MonoBehaviour ,ICharacter
{

    public Character MyCharacter;
    public Transform CameraTransform;


    private ThirdPersonAnimation _thirdPersonAnimation;
    private ThirdPersonUserControl _thirdPersonUserControl;

    [SerializeField] private Slider _slider;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
 

    private WeaponMono _weaponMono;

    private PowerUpInventory _powerUpInventory;


    private RaycastHit _raycastHit;        
    
    
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendererOne;
    [SerializeField] private SkinnedMeshRenderer _skinnedMeshRendererTwo;
    private MaterialPropertyBlock _materialProperty;

    private void Awake()
    {
        _thirdPersonUserControl = GetComponent<ThirdPersonUserControl>();
        _thirdPersonAnimation = GetComponent<ThirdPersonAnimation>();
        _weaponMono = GetComponent<WeaponMono>();
        _powerUpInventory = GetComponent<PowerUpInventory>();
                
        MyCharacter = new Character(10,10,10,10,10);
        InitializePropertyBlock();
    }

    private void InitializePropertyBlock()
    {
        _materialProperty = new MaterialPropertyBlock();
        _skinnedMeshRendererOne.SetPropertyBlock(_materialProperty);
        _skinnedMeshRendererTwo.SetPropertyBlock(_materialProperty);
    }

    public void SetPropertyColor(Color color)
    {
        _materialProperty.SetColor("_Color", color);
        _skinnedMeshRendererOne.SetPropertyBlock(_materialProperty);
        _skinnedMeshRendererTwo.SetPropertyBlock(_materialProperty, 0);
    }

    public int OwnedBy()
    {
        return MyCharacter.OwnedBy();

    }

    public void SetHealth(float damage)
    {
        MyCharacter.SetHealth(damage);
    }

    public void SetSliderValue()
    {
        Debug.Log(MyCharacter.Health);
        
        _slider.value = MyCharacter.Health;
    }

    public void Die()
    {
        _thirdPersonAnimation.Die();
        PlayerManager.Instance.GetCurrentlyActivePlayer().RemoveCharacter(MyCharacter);
        RemoveSlider();
    }
    
    
    //[0] = leftHand, [1] = rightHand
    
    public Transform[] GetRightAndLeftHand()
    {
        return new[] {_leftHand, _rightHand};
    }


    private void Update()
    {
        Vector3 vector3 = Camera.main.transform.position - _slider.transform.position;
        vector3.x = vector3.z = 0.0f;
        _slider.transform.LookAt(Camera.main.transform.position - vector3);
        _slider.transform.Rotate(0,180,0);
    }

    private void RemoveSlider()
    {
       Destroy(_slider.gameObject);
       Destroy(this);
    }

    private void Start()
    {
        _slider.value = MyCharacter.Health;
    }

    public void HandleAttack(RaycastHit hit)
    {
        var hitChar = hit.collider.GetComponent<ICharacter>();

        if (hitChar != null)
        {
            if (GameManager.Instance.FriendlyFire)
            {
                hitChar.SetHealth((MyCharacter.Strength / 50)    * (_weaponMono.MyWeapon.Damage + MyCharacter.Defence / 50));
            }
        }
    }



    public void FortifyAnimation(bool value)
    {
        _thirdPersonAnimation.Fortify(value);
        
    }

    public int GetCurrentAmountOfAdrenaline()
    {
        return _powerUpInventory.GetCurrentAmountOfAdrenaline();
    }

    public int GetCurrentAmountOfTimeMachine()
    {
        return _powerUpInventory.GetCurrentAmountOfTimeMachine();

    }

    public int GetCurrentAmountOfRage()
    {
        return _powerUpInventory.GetCurrentAmountOfRage();
    }

    public void ActivatePowerUp(PowerUp.PowerUpEnum powerUpEnum)
    {
       _powerUpInventory.ActivatePowerUp(powerUpEnum);
    }


    public void DisableUserControl()
    {
        _thirdPersonUserControl.enabled = false;
        _thirdPersonAnimation.ResetForward();
        _slider.gameObject.SetActive(true);

    }

    public void EnableUserControl()
    {
        _thirdPersonUserControl.enabled = true;
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.InSelectionState() && MyCharacter.OwnedByPlayer == PlayerManager.Instance.GetCurrentlyActivePlayer().PlayerNumber)
        {
            
            Debug.Log("Click");
            GameManager.Instance.SetCameraMovement(CameraTransform, true);
            _slider.gameObject.SetActive(false);
            GameManager.Instance.EnableHealthSlider();
        }
    }
    
    public bool OnFloor()
    {
        Ray myRay = new Ray(transform.position, -transform.up);
        if (!Physics.Raycast(myRay, out _raycastHit, 50f)) return false;
        return _raycastHit.collider.CompareTag("Terrain");
    }








}
