using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUICustomStats : MonoBehaviour
{
    public Slider healthSlider, strengthSlider, speedSlider, defenseSlider;
    public ushort health, speed, strength, defense, cost;

    public Text buttonText;

    public void Start()
    {
        healthSlider.value = Random.Range(0.1f, 1f);
        speedSlider.value = Random.Range(0.01f, 0.7f);
        strengthSlider.value = Random.Range(0.01f, 0.7f);
        defenseSlider.value = Random.Range(0.01f, 0.7f);
    }

    public void MathfUnitCost ()
    {
        //Strength and defense are cheap
        cost = (ushort)(health + speed + Mathf.RoundToInt((strength + defense) / 2f));
        buttonText.text = "Create ($" + cost + ")";
    }

    public void OnStatsChanged ()
    {
        health = (ushort)(healthSlider.value * 100);
        speed = (ushort)(speedSlider.value * 100);
        strength = (ushort)(strengthSlider.value * 100);
        defense = (ushort)(defenseSlider.value * 100);
        MathfUnitCost();
    }

    public SoldierAsset CreateAsset()
    {
        SoldierAsset asset = ScriptableObject.CreateInstance<SoldierAsset>();
        asset.unitSoldier = new Soldier();
        asset.unitSoldier.CreateUnit(health, strength, speed, defense, cost);

        return asset;
    }
}
