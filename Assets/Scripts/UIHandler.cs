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
    public Button turnDoneButton;
    public Color turnDone, turnUnDone;
    public Slider attackTimeSlider;
    public GameObject UnitPanel;
    public Slider health;
    public TMP_Text strenght, defence, speed, Weapon;

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

        if(player.controlState == ControlState.Selected || player.controlState == ControlState.Controlling)
        {
            health.value = player.curUnit.m_Health;
            strenght.text = "Str: " + player.curUnit.m_Strength;
            defence.text = "Def: " + player.curUnit.m_Defence;
            speed.text = "Spd: " + player.curUnit.m_Speed;
            Weapon.text = player.curUnit.weapon.name;
        }

        if(player.controlState == ControlState.None)
        {
            UnitPanel.SetActive(false);
            turnDoneButton.gameObject.SetActive(true);
            var colors = turnDoneButton.colors;
            colors.normalColor = player.doEndTurn ? turnDone : turnUnDone;
            turnDoneButton.colors = colors;
        }

        if (player.controlState != ControlState.None)
        {
            turnDoneButton.gameObject.SetActive(false);
            UnitPanel.SetActive(true);
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

        if(player.firstPower != null)
        {
            if (player.firstPower.power == Power.Adrenaline)
            {
                nextPowerText.text = "Adrenaline";
            }
            else if (player.firstPower.power == Power.Rage)
            {
                nextPowerText.text = "Rage";
            }
            else if (player.firstPower.power == Power.TimeMachine)
            {
                nextPowerText.text = "Time machine";
            }
        }
        else
        {
            nextPowerText.text = "";
        }

        attackTimeSlider.gameObject.SetActive(player.isAttacking);

        if (player.isAttacking)
        {
            attackTimeSlider.maxValue = player.GetChargeTime();
            attackTimeSlider.value = (player.myAttackTime - Time.time);
        }
    }

    public void ResetDone()
    {
        turnDoneButton.gameObject.SetActive(false);
        var colors = turnDoneButton.colors;
        colors.normalColor = player.doEndTurn ? turnDone : turnUnDone;
        turnDoneButton.colors = colors;
    }
}
