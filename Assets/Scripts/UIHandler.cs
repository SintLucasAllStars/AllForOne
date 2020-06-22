using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    private Player player;
    public Slider controlTimeSlider;
    public GameObject powerUpAdrenaline, powerUpRage, powerUpTime;
    public Slider powerUpAdrenalineSlider, powerUpRageSlider, powerUpTimeSlider;
    public TMP_Text nextPowerText;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if(player.controlState == ControlState.Controlling)
        {
            controlTimeSlider.gameObject.SetActive(true);
            controlTimeSlider.value = player.myDeltaTime - Time.time;
        }

        if (player.controlState != ControlState.Controlling)
        {
            controlTimeSlider.gameObject.SetActive(false);
        }

        powerUpAdrenaline.SetActive(player.adrenalinePower);
        powerUpRage.SetActive(player.ragePower);
        powerUpTime.SetActive(player.timeMachinePower);

        if (powerUpAdrenaline.activeSelf)
        {
            powerUpAdrenalineSlider.value = (player.myAdrenalineTime - Time.time);
        }

        if (powerUpRage.activeSelf)
        {
            powerUpRageSlider.value = (player.myRageTime - Time.time);
        }

        if (powerUpTime.activeSelf)
        {
            powerUpTimeSlider.value = (player.myTimeMachineTime - Time.time);
        }

        if (player.firstPower.power == Power.Adrenaline)
        {

        }
        else if (player.firstPower.power == Power.Rage)
        {

        }
        else if (player.firstPower.power == Power.TimeMachine)
        {

        }
    }
}
