using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    UnitClass unit;

    public float Health => unit.GetHealth();
    public float Strength => unit.GetStrength();
    public float Speed => unit.GetSpeed();
    public float Defence => unit.GetDefence();

    // Start is called before the first frame update
    void Start()
    {
        unit = new UnitClass(UIManager.instance.GetHealth(), UIManager.instance.GetStrength(), UIManager.instance.GetSpeed(), UIManager.instance.GetDefence());
    }

    // Update is called once per frame
    void Update()
    {
        print(Speed);
        print(Strength);
        print(Speed);
        print(Defence);
    }
}
