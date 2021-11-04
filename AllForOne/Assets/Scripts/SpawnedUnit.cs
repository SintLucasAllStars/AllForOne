using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedUnit : MonoBehaviour
{
    private Unit unit;
    private UIManager ui;

    public int Health => unit.GetHealth();
    public int Strenght => unit.GetStrenght();
    public int Speed => unit.GetSpeed();
    public int Defence => unit.GetDefence();

    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        unit = new Unit(ui.GetHealthValue(), ui.GetStrenghtValue(), ui.GetSpeedValue(), ui.GetDefenceValue());
    }

    private void Update()
    {
        Debug.Log(Health);
        Debug.Log(Strenght);
        Debug.Log(Speed);
        Debug.Log(Defence);
    }
}
