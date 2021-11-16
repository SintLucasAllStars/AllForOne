using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnedUnit : MonoBehaviour
{
    private Unit unit;
    private UIManager ui;

    private GameObject mainCamera;

    public Vector3 offset;

    public int Health => unit.GetHealth();
    public int Strength => unit.GetStrength();
    public int Speed => unit.GetSpeed();
    public int Defence => unit.GetDefence();

    private int health;

    private void Start()
    {
        ui = GameObject.Find("UIManager").GetComponent<UIManager>();

        unit = new Unit(ui.GetHealthValue(), ui.GetStrengthValue(), ui.GetSpeedValue(), ui.GetDefenceValue());

        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        GameManager.instance.AddUnit(gameObject);

        this.gameObject.GetComponent<SpawnedUnit>().enabled = false;

        health = Health;

        if (GameManager.instance.UnitsPlayer_1.Contains(this.gameObject))
        {
            this.gameObject.GetComponent<Material>().color = Color.green;
        }

        if (GameManager.instance.UnitsPlayer_2.Contains(this.gameObject))
        {
            this.gameObject.GetComponent<Material>().color = Color.red;
        }
    }

    private void Update()
    {
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, gameObject.transform.position + offset, 0.125f);
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal") * Time.deltaTime * Speed / 2;
        float z = Input.GetAxis("Vertical") * Time.deltaTime * Speed / 2;

        transform.Translate(h, 0, z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.playerTurn)
        {
            
        }
    }
}
