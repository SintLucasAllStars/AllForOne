using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    private UnitInterface _unitInterface;
    private PlayerManager _playerManager;
    private UnitSelectionManager _unitSelectionManager;

    public Slider HealthSlider;

    [SerializeField]
    private WeaponObject[] _holdingWeapons;

    public PowerUpInfo UnitPowerUpInfo;
    public WeaponObject CurrentUnitWeaponObject;

    public Stats UnitStats;

    public Transform WeaponHolder;

    //private bool _isGrounded = true;
    private bool _freezeCounter = false;

    public bool IsUnitActive { get; set; }
    public int UnitTeamId { get; private set; }

    public float RotationSpeed;

    private void Start()
    {
        CurrentUnitWeaponObject = _holdingWeapons[0];

        _unitInterface = FindObjectOfType<UnitInterface>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _unitSelectionManager = FindObjectOfType<UnitSelectionManager>();
    }

    public void InitializeUnit(int unitTeamId, Stats unitStats)
    {
        UnitTeamId = unitTeamId;
        IsUnitActive = false;
        UnitStats = unitStats;

        HealthSlider.maxValue = UnitStats.Health;
        HealthSlider.value = UnitStats.Health;
    }

    public void TakeDamage(float damage)
    {
        if ((UnitStats.Health -= (damage / UnitStats.Defense)) <= 0)
            Die();

        HealthSlider.value = UnitStats.Health;
    }

    public void Die()
    {
        _playerManager.GetOtherPlayer.RemoveUnit(this);
        Destroy(gameObject);
        _playerManager.GameOver();
    }

    private void Update()
    {
        if (IsUnitActive)
        {
            if (!_freezeCounter)
            {
                GameManager.Instance.PlayerTime -= Time.unscaledDeltaTime;

                _unitInterface.SetGameTime((int)GameManager.Instance.PlayerTime);

                if ((int)GameManager.Instance.PlayerTime <= 0)
                {
                    IsUnitActive = false;

                    GameManager.Instance.PlayerTime = 10f;

                    _unitInterface.SetGameTime((int)GameManager.Instance.PlayerTime);

                    IsOutside();

                    //Turn over, set next player turn.
                    _playerManager.SetNextPlayerIndex();

                    _unitInterface.DisableUnitInterface();

                    StartCoroutine(_unitSelectionManager.UnitSelection());
                }

            }

            float translation = Input.GetAxis("Vertical") * UnitStats.Speed * 0.5f;
            float rotation = Input.GetAxis("Horizontal") * UnitStats.Speed * RotationSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            transform.Translate(0, 0, translation);

            transform.Rotate(0, rotation, 0);

            if (Input.GetKeyDown(KeyCode.Q))
                StartCoroutine(UsePowerUp());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsUnitActive)
        {
            if (other.CompareTag("PowerUp") && Input.GetKeyDown(KeyCode.E))
            {
                UnitPowerUpInfo = other.gameObject.GetComponent<PowerUp>().PowerUpInfo;
                _unitInterface.SetInterfacePowerUp(UnitPowerUpInfo);
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Weapon") && Input.GetKeyDown(KeyCode.F))
            {
                CurrentUnitWeaponObject.UnEquipWeapon();

                WeaponObject weaponObject = other.GetComponent<WeaponObject>();

                _unitInterface.SeInterfaceUnitWeapon(weaponObject.WeaponInfo);

                int index = (int)weaponObject.WeaponInfo.weaponType;

                CurrentUnitWeaponObject = _holdingWeapons[index].EquipWeapon();

                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator UsePowerUp()
    {
        float enhancement = UnitPowerUpInfo.PowerUpEnhancement;
        float duration = UnitPowerUpInfo.PowerUpDuration;

        switch (UnitPowerUpInfo.PowerType)
        {
            case PowerUpType.Adrenaline:
                UnitStats.Speed += UnitStats.Speed * enhancement;
                yield return StartCoroutine(_unitInterface.PowerUpTime(duration));
                UnitStats.Speed -= UnitStats.Speed * enhancement;
                break;
            case PowerUpType.Rage:
                UnitStats.Strenght += UnitStats.Strenght * enhancement;
                yield return StartCoroutine(_unitInterface.PowerUpTime(duration));
                UnitStats.Strenght -= UnitStats.Strenght * enhancement;
                break;
            case PowerUpType.TimeMachine:
                _freezeCounter = true;
                yield return StartCoroutine(_unitInterface.PowerUpTime(duration));
                _freezeCounter = false;
                break;
            case PowerUpType.None:
                break;
        }

        UnitPowerUpInfo.PowerType = PowerUpType.None;
        _unitInterface.SetInterfacePowerUp(UnitPowerUpInfo);
    }
    public IEnumerator RayCastWeapon()
    {
        int unitLayerMask = LayerMask.GetMask("UnitLayer");

        while (IsUnitActive)
        {
            Ray ray = new Ray(transform.position, transform.forward);

            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * CurrentUnitWeaponObject.WeaponInfo.Range * 1.5f, Color.green);

            if (Physics.Raycast(ray, out hit, CurrentUnitWeaponObject.WeaponInfo.Range * 1.5f, unitLayerMask))
            {
                Unit unit = hit.collider.GetComponent<Unit>();

                if (unit.UnitTeamId != UnitTeamId && Input.GetMouseButtonDown(0))
                {
                    unit.TakeDamage(CurrentUnitWeaponObject.WeaponInfo.Damage * UnitStats.Strenght);
                    yield return StartCoroutine(_unitInterface.RechargeInterfaceWeapon(CurrentUnitWeaponObject.WeaponInfo.Speed));
                }
            }

            yield return null;
        }
    }

    public void IsOutside()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.CompareTag("Outside"))
            {
                Camera.main.transform.SetParent(_unitSelectionManager.CamTransform);
                
                _playerManager.GetCurrentPlayer.RemoveUnit(this);

                Destroy(gameObject);

                _playerManager.GameOver();
            }
        }
    }
}

[System.Serializable]
public struct Stats
{
    public float Health;
    public float Strenght;
    public float Speed;
    public float Defense;
}
