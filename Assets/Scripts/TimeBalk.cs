using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeBalk : MonoBehaviour
{

    public float timeRemaining;
    public float maxTime = 10f;
    public Slider slider;

    public CameraMove cmScript;

    public TMP_Text playerTurnText;
    public bool dead;

    int a = 1;
    int b = 2;

    public bool walkOn;
    public bool test = true;

    void Start()
    {
        timeRemaining = 10f;
    }

    void Update()
    {

        slider.value = CalculateSliderValue();

        if (timeRemaining <= 0)
        {
            StartCoroutine(WaitForHealth());
        }
        else if (timeRemaining > 0 && walkOn == true && test == true)
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

        IEnumerator Wait()
        {
            cmScript.psScript.testHit = true;
            test = false;
            timeRemaining = 10;
            yield return new WaitForSeconds(3);
            cmScript.BackToTop();
            timeRemaining = 10;
            walkOn = false;
            test = false;
        }

        IEnumerator WaitForHealth()
        {
            yield return new WaitForSeconds(0.5f);
        GO();

        }

    public void GO()
    {
        Cursor.lockState = CursorLockMode.None;
        if (GameObject.FindGameObjectsWithTag("u_Player1").Length <= 1 && dead == true)
        {
            Debug.Log("Player 1 won");
            return;
        }
        else if (GameObject.FindGameObjectsWithTag("u_Player2").Length <= 1 && dead == true)
        {
            Debug.Log("Player 2 won");
            return;
        }
        else if (cmScript.psScript.testHit == false && cmScript.pcScript.e_health <= 0)
        {
            SwapNum(ref a, ref b);
            Debug.Log("TestHitFalse");
            walkOn = false;
            //timeRemaining = 10;
            playerTurnText.text = ("Player ") + a + (" turn");
            StartCoroutine(Wait());
            return;
        }
        else if (cmScript.psScript.testHit == true && test && cmScript.pcScript.e_health > 0 && walkOn == true)
        {
            SwapNum(ref a, ref b);
            Debug.Log("TestHitTrue");
            walkOn = false;
            playerTurnText.text = ("Player ") + a + (" turn");
            test = false;
            timeRemaining = 10;
            cmScript.BackToTop();
            timeRemaining = 10;
            walkOn = false;
            test = true;
            timeRemaining = 10;
            return;
        }
    }

    public void Change()
    {
        SwapNum(ref a, ref b);
        playerTurnText.text = ("Player ") + a + (" turn");
    }
}
