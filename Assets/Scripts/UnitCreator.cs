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

    private void OnEnable()
    {
        GameManager.instance.OnGameStart += DeActive;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStart -= DeActive;
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
        unit = Instantiate(unitPrefab) as GameObject;

        foreach (Renderer item in unit.GetComponentsInChildren<Renderer>())
        {
            item.material.SetColor("_EmissionColor", player.color);
        }

        unit.GetComponent<Collider>().enabled = false;

        ShowRoom.instance.DisplayObject(unit);
        unit.SetActive(false);
    }

    /// <summary>
    /// Preview the Unit at given location.
    /// </summary>
    public void PreviewUnit(Vector3 position)
    {
        if(unit)
        {
            Unit.SetActive(true);
            Unit.transform.position = position;
            unit.transform.Rotate(0, Input.GetAxis("RotatePreviewUnit") * 2.5f, 0);
        }
    }

    /// <summary>
    /// Spawn the unit with stats from the creator on given location.
    /// </summary>
    public void SpawnUnit(Vector3 position)
    {
        Unit _unit = unit.GetComponent<Unit>();
        _unit.owner             = player.index;
        _unit.stats.health      = health.slider.value;
        _unit.stats.strength    = strength.slider.value;
        _unit.stats.speed       = speed.slider.value;
        _unit.stats.defence     = defence.slider.value;

        unit.SetActive(true);
        unit.name = "Unit_Player_" + player.index;
        unit.GetComponent<Collider>().enabled = true;
        unit = null;
    }

    private void CalculateCost()
    {
        int a = Mathf.CeilToInt(Math.Map(health.slider.value, health.slider.minValue, health.slider.maxValue, 3, 30));
        int b = Mathf.CeilToInt(Math.Map(speed.slider.value, speed.slider.minValue, speed.slider.maxValue, 3, 30));
        int c = Mathf.CeilToInt(Math.Map(strength.slider.value, strength.slider.minValue, strength.slider.maxValue, 2, 20));
        int d = Mathf.CeilToInt(Math.Map(defence.slider.value, defence.slider.minValue, defence.slider.maxValue, 2, 20));

        cost = a + b + c + d;
        tCost.text = "Cost: " + cost.ToString();
    }

    private void DeActive()
    {
        Destroy(gameObject);
    }

    public void ButtonReady()
    {
        player.Ready = true;
    }
}
