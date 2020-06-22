using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    [SerializeField] private float strength;
    [SerializeField] private float speed;
    [SerializeField] private float defense;
    [SerializeField] private float health;

    [SerializeField] private UnitCreationScreen UnitMaker;
    [SerializeField] private GameManager GM_script;

    [SerializeField] private Material red;
    [SerializeField] private Material blue;

    [SerializeField] private MeshRenderer thisMesh;

    // Start is called before the first frame update
    void Start()
    {
        GM_script = GameObject.Find("ETC").GetComponent<GameManager>();
        UnitMaker = GameObject.Find("UnitCreationPanel").GetComponent<UnitCreationScreen>();

        if (GM_script.player1_Active == true)
        {
            thisMesh.material = red;
        }
        else
        {
            thisMesh.material = blue;
        }

        strength = Mathf.RoundToInt(UnitMaker.strengthSlider.value);
        speed = Mathf.RoundToInt(UnitMaker.speedSlider.value);
        health = Mathf.RoundToInt(UnitMaker.healthSlider.value);
        defense = Mathf.RoundToInt(UnitMaker.defenseSlider.value);
    }
}
