using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected enum UnitState {Idle, Attacking, Dead}

    //Unit Transform Variables
    public Transform cameraTransform;
    public Transform weaponTransform;

    //Standard Combat Variables
    protected readonly int HitPoints = 100;
    protected int speed = 10;
    protected int strength = 10;
    protected int defense = 10;

    protected int teamNumber;

    protected Weapon weapon;

    //Movement Variables
    protected Rigidbody rb;
    protected Vector3 direction;

    //Active Turn Variables
    protected bool isSelected;

    //Special Features Variables
    protected bool hasDriveby = false;
    protected bool hasOpportunist = false;
    protected bool hasTowershield = false;

    /// <summary>
    /// Places a unit on the map.
    /// </summary>
    public void CreateUnit(int speed, int strength, int defense, int teamNumber, List<Feature> features, Weapon weapon)
    {
        this.speed = speed;
        this.strength = strength;
        this.defense = defense;

        this.teamNumber = teamNumber;

        this.weapon = weapon;

        CheckSpecialFeatures(features);
    }

    /// <summary>
    /// Called from GameManager, when this unit starts his turn.
    /// </summary>
    public void StartSelectedTurn()
    {
        isSelected = true;
    }

    /// <summary>
    /// Lets the Unit move using the unit's speed. Is only active when it's the unit it's turn.
    /// </summary>
    protected void Movement()
    {
        rb.velocity = direction * speed * Time.deltaTime;

        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        direction = (horizontalMovement * transform.right + verticalMovement * transform.forward).normalized;
    }

    /// <summary>
    /// Activates the Special features that were bought in the UnitStore.
    /// </summary>
    protected void CheckSpecialFeatures(List<Feature> feats)
    {
        for (int i = 0; i < feats.Count - 1; i++)
        {
            if (feats[i].feat == UnitStore.BonusFeatures.Driveby)
            {
                hasDriveby = true;
            }
            if (feats[i].feat == UnitStore.BonusFeatures.Opportunist)
            {
                hasOpportunist = true;
            }
            if (feats[i].feat == UnitStore.BonusFeatures.TowerShield)
            {
                hasTowershield = true;
            }
        }
    }
}