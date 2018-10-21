using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitInterface _unitInterface;
    private PlayerManager _playerManager;
    private UnitSelectionManager _unitSelectionManager;

    public PowerUp UnitPowerUp;
    public WeaponObject UnitWeapon;
    public Stats UnitStats;

    public Transform WeaponHolder;

    public bool IsUnitActive { get; set; }
    public int UnitTeamId { get; private set; }

    private float _rotationSpeed;

    private void Start()
    {
        _unitInterface = FindObjectOfType<UnitInterface>();
    }

    public void InitializeUnit(int unitTeamId, Stats unitStats)
    {
        UnitTeamId = unitTeamId;
        IsUnitActive = false;
        UnitStats = unitStats;
    }

    public void TakeDamage(float damage)
    {
        if ((UnitStats.Health -= (damage / UnitStats.Defense)) <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void EquipWeapon(WeaponObject weaponObject)
    {
        UnitWeapon = weaponObject;
        weaponObject.GetComponent<Rigidbody>().useGravity = false;
        weaponObject.transform.SetParent(WeaponHolder);
        weaponObject.InitializeWeapon(this);
    }

    public void UnEquipWeapon()
    {
        UnitWeapon.IsWeaponActive = false;
        UnitWeapon.transform.parent = null;
        UnitWeapon.GetComponent<Rigidbody>().useGravity = true;
    }

    private void Update()
    {
        if (IsUnitActive)
        {
            float time = GameManager.Instance.PlayerTime -= Time.unscaledDeltaTime;

            if (time <= 0)
            {
                IsUnitActive = false;

                GameManager.Instance.PlayerTime = 10;

                _playerManager.SetNextPlayerIndex();

                StartCoroutine(_unitSelectionManager.UnitSelection());
            }

            float translation = Input.GetAxis("Vertical") * UnitStats.Speed;
            float rotation = Input.GetAxis("Horizontal") * UnitStats.Speed * _rotationSpeed;

            translation *= Time.deltaTime;
            rotation *= Time.deltaTime;

            transform.Translate(0, 0, translation);

            transform.Rotate(0, rotation, 0);

            if (Input.GetKeyDown(KeyCode.Q))
                StartCoroutine(UsePowerUp());

            if (Input.GetKeyDown(KeyCode.C))
                UnEquipWeapon();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsUnitActive)
        {
            if (other.CompareTag("PowerUp") && Input.GetKeyDown(KeyCode.E))
            {
                UnitPowerUp = other.gameObject.GetComponent<PowerUp>();
                _unitInterface.SetInterfacePowerUp(UnitPowerUp.PowerType);
                Destroy(other.gameObject);
            }

            if (other.CompareTag("Weapon") && Input.GetKeyDown(KeyCode.F))
            {
                WeaponObject weaponObject = other.GetComponent<WeaponObject>();

                if (UnitWeapon == null)
                {
                    EquipWeapon(weaponObject);
                }
                else
                {
                    UnEquipWeapon();
                    EquipWeapon(weaponObject);
                }
            }
        }
    }

    private IEnumerator UsePowerUp()
    {
        float enhancement = UnitPowerUp.PowerUpEnhancement;
        float duration = UnitPowerUp.PowerUpDuration;

        switch (UnitPowerUp.PowerType)
        {
            case PowerUpType.Adrenaline:
                UnitStats.Speed += UnitStats.Speed * enhancement;
                yield return new WaitForSeconds(duration);
                UnitStats.Speed -= UnitStats.Speed * enhancement;
                break;
            case PowerUpType.Rage:
                UnitStats.Strenght += UnitStats.Strenght * enhancement;
                yield return new WaitForSeconds(duration);
                UnitStats.Strenght -= UnitStats.Strenght * enhancement;
                break;
            case PowerUpType.TimeMachine:
                yield return new WaitForSeconds(duration);
                break;
            case PowerUpType.None:

                break;
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
