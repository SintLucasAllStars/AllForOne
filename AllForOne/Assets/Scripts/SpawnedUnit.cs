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

        GameManager.instance.AddUnit(gameObject);
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * Speed;

        transform.Translate(h, 0, z);
    }
}
