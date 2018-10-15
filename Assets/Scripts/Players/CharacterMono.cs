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

    [SerializeField] private List<BoxCollider> _boxColliders = new List<BoxCollider>();
    [SerializeField] private Slider _slider;


    private WeaponMono _weaponMono;




    private void Awake()
    {
        _thirdPersonUserControl = GetComponent<ThirdPersonUserControl>();
        _thirdPersonAnimation = GetComponent<ThirdPersonAnimation>();
        _weaponMono = GetComponent<WeaponMono>();


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
                hitChar.SetHealth(MyCharacter.Strength / 100 * _weaponMono.MyWeapon.Damage);
            }
        }
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






}
