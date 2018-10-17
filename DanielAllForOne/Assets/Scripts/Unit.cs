using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private UnitInterface _unitInterface;

    private void Start()
    {
        _unitInterface = FindObjectOfType<UnitInterface>();
    }

    public void InitializeUnit(int unitTeamId, float[] unitStats)
    {
        GetUnitTeamId = unitTeamId;
        UnitStats = unitStats;
        IsUnitActive = false;
    }

    public PowerUp UnitPowerUp { get; set; }
    public bool IsUnitActive { get; set; }
    public int GetUnitTeamId { get; private set; }
    public float[] UnitStats { get; private set; }

    private void OnTriggerStay(Collider other)
    {
        if (IsUnitActive)
        {
            if (other.gameObject.CompareTag("PowerUp") && Input.GetKeyDown(KeyCode.E))
            {
                UnitPowerUp = other.gameObject.GetComponent<PowerUp>();
                _unitInterface.SetCurrentUnitPowerUp(UnitPowerUp.PowerType);
                Destroy(other.gameObject);
            }
        }
    }
}
