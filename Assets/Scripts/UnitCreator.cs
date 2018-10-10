using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreator : MonoBehaviour
{
    [System.Serializable]
    internal struct SliderElement
    {
        public Slider slider;
        public Text textMin;
        public Text textMax;
    }

    public GameObject Unit { get { return unit; } } 
    public Player Player { get { return player; } set { player = value; CreateUnit(); } }
    public int Cost { get { return cost; } }

    public static UnitCreator instance;

    [SerializeField] private GameObject unitPrefab = null;

    [Header("UI")]
    [SerializeField] private Text tPlayerName;
    [SerializeField] private Text tCost, remaining;
    [SerializeField] private SliderElement health, strength, speed, defence;
    [SerializeField] private RawImage preview;

    private Player player = null;
    private GameObject unit = null;
    private int cost = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        health.textMin.text     = health.slider.value.ToString();
        strength.textMin.text   = strength.slider.value.ToString();
        speed.textMin.text      = speed.slider.value.ToString();
        defence.textMin.text    = defence.slider.value.ToString();

        remaining.text = player.points.ToString();
        tPlayerName.text = player.name;

        CalculateCost();
    }

    private void CreateUnit()
    {
        GameObject _unit = Instantiate(unitPrefab) as GameObject;

        foreach (Renderer item in _unit.GetComponentsInChildren<Renderer>())
        {
            item.material.SetColor("_EmissionColor", player.color);
        }

        unit = _unit;
        ShowRoom.instance.DisplayObject(unit);
        unit.SetActive(false);
    }

    private void CalculateCost()
    {
        int a = Mathf.CeilToInt(Remap(health.slider.value, health.slider.minValue, health.slider.maxValue, 3, 30));
        int b = Mathf.CeilToInt(Remap(speed.slider.value, speed.slider.minValue, speed.slider.maxValue, 3, 30));
        int c = Mathf.CeilToInt(Remap(strength.slider.value, strength.slider.minValue, strength.slider.maxValue, 2, 20));
        int d = Mathf.CeilToInt(Remap(defence.slider.value, defence.slider.minValue, defence.slider.maxValue, 2, 20));

        cost = a + b + c + d;
        tCost.text = "Cost: " + cost.ToString();
    }

    float Remap(float value, float inputFrom, float inputTo, float outputFrom, float outputTo)
    {
        return outputFrom + (value - inputFrom) * (outputTo - outputFrom) / (inputTo - inputFrom);
    }

    public void ButtonReady()
    {
        player.Ready = true;
    }
}
