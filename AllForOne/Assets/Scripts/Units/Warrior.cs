using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Unit
{

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Called when an enemy is within the Weapon's range.
    /// </summary>
    protected override void StartCombat()
    {
        base.StartCombat();

        //Calculate Damage
        unitInterface.DamageText(CalculateDamage());
    }

    /// <summary>
    /// Called when the Attack button is clicked.
    /// </summary>
    public void ButtonBeginAttack()
    {
        isAttacking = true;
        float waitTime = 11 - weapon.speed;

        //animation
        unitAnimation.AnimAttack(true);

        GameManager.gameManager.UnitAttacking(waitTime);
    }

    /// <summary>
    /// Called after the delayTimer from GameManager has ended.
    /// </summary>
    public override void EndAttack()
    {
        base.EndAttack();
        if (target.LethalDamage(CalculateDamage()))
        {
            target = null;
        }
        isAttacking = false;
        EndCombat(2);
    }

    /// <summary>
    /// Calculate the damage that you will do by taking your weapon.damage and strength divided by their defense.
    /// </summary>
    private int CalculateDamage()
    {
        int damage;

        float rawDamage = weapon.damage * (strength / 4);
        float resistance = 1.00f + (defense / 100);

        float calculatedDamage = rawDamage / resistance;

        damage = Mathf.RoundToInt(calculatedDamage);

        return damage;
    }
}
