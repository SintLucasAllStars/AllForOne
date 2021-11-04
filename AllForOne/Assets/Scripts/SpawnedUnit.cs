using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedUnit : MonoBehaviour
{
    private Unit unit;
    private UIManager ui;

    public int Health => unit.GetHealth();
    public int Strength => unit.GetStrength();
    public int Speed => unit.GetSpeed();
    public int Defence => unit.GetDefence();

    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        unit = new Unit(ui.GetHealthValue(), ui.GetStrengthValue(), ui.GetSpeedValue(), ui.GetDefenceValue());
    }

    private void Update()
    {
        Debug.Log(Health);
        Debug.Log(Strength);
        Debug.Log(Speed);
        Debug.Log(Defence);
    }
}
