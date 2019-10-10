using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit
{
    private enum UnitState {Idle, Attacking, Dead}

    private Weapon weapon;

    private readonly int HitPoints = 100;
    private int speed;
    private int strength;
    private int defense;

    private int teamNumber;

    private bool inCombat;

    //Special Features 
    private bool hasDriveby = false;
    private bool hasOpportunist = false;
    private bool hasTowershield = false;


    public void CreateUnit(int speed, int strength, int defense, List<Feature> features, Weapon weapon)
    {
        this.speed = speed;
        this.strength = strength;
        this.defense = defense;

        this.weapon = weapon;

        CheckSpecialFeatures(features);
    }

    private void CheckSpecialFeatures(List<Feature> feats)
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