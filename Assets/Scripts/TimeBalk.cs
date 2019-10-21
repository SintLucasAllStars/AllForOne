using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBalk : MonoBehaviour
{

    private float timeRemaining;
    public float maxTime = 10f;
    public Slider slider;

    public CameraMove cmScript;

    public TMP_Text playerTurnText;

    int a = 1;
    int b = 2;

    void Start()
    {
        timeRemaining = maxTime;
    }

    void Update()
    {
        slider.value = CalculateSliderValue();

        if(timeRemaining <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            timeRemaining = 10;
            SwapNum(ref a, ref b);
            playerTurnText.text = ("Player ") + a + (" turn");
            cmScript.BackToTop();
            return;
        }
        else if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    float CalculateSliderValue()
    {
        return (timeRemaining / maxTime);
    }

    public void SwapNum(ref int x, ref int y)
    {
        int tempswap = x;
        x = y;
        y = tempswap;
    }
}
