using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UnitCreationScreen : MonoBehaviour
{
    [SerializeField] private Text totalScoreText;
    [SerializeField] private Text healthSliderText;
    [SerializeField] private Text speedSliderText;
    [SerializeField] private Text defenseSliderText;
    [SerializeField] private Text strengthSliderText;

    [SerializeField] public Slider strengthSlider;
    [SerializeField] public Slider speedSlider;
    [SerializeField] public Slider defenseSlider;
    [SerializeField] public Slider healthSlider;

    [SerializeField] GameManager GM_script;

    [SerializeField] private GameObject objectUnit;

    [SerializeField] private Vector3[] player1Spawns;
    [SerializeField] private Vector3[] player2Spawns;

    private float totalScoreCost;

    // Update is called once per frame
    void Update()
    {
        totalScoreCost = (strengthSlider.value * 2) + (defenseSlider.value * 2) + (healthSlider.value * 3) + (speedSlider.value * 3);
        totalScoreText.text = "Cost: " + Mathf.RoundToInt(totalScoreCost);

        healthSliderText.text = "Health: " + Mathf.RoundToInt(healthSlider.value);
        speedSliderText.text = "Speed: " + Mathf.RoundToInt(speedSlider.value);
        strengthSliderText.text = "Strength: " + Mathf.RoundToInt(strengthSlider.value);
        defenseSliderText.text = "Defense: " + Mathf.RoundToInt(defenseSlider.value);
    }

    public void BuyUnit()
    {
        if(GM_script.currentPoints > totalScoreCost || GM_script.currentPoints == totalScoreCost)
        {
            GM_script.currentPoints -= totalScoreCost;
            GM_script.pointsDisplayer.text = "Points: " + Mathf.RoundToInt(GM_script.currentPoints);

            if(GM_script.player1_Active == true)
            {
                Instantiate(objectUnit, player1Spawns[GM_script.currentSpawn], Quaternion.identity);
                GM_script.currentSpawn++;
            }
            else
            {
                Instantiate(objectUnit, player2Spawns[GM_script.currentSpawn], Quaternion.identity);
                GM_script.currentSpawn++;
            }
            return;
        }

        if (GM_script.currentPoints < totalScoreCost)
        {
            Debug.Log("can't buy it");
            return;
        }
    }
}
