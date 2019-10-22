using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValue : MonoBehaviour
{
    public Slider[] sliderUI;
    public TMP_Text[] textSliderValue;
    public TMP_Text textTotalPoints, textTotalPointsValue;

    public int[] sliderValue3;
    public int[] sliderValue2;
    public int totalPriceValue;

    public Button next;

    public GameObject optionsMenu;

    public int playerOnePoints, playerTwoPoints = 100;

    public bool switchTurn, playerOneDone;

    public bool playerTwoDone;
    public bool raycastOn = true;

    public MeshCollider[] groundCollider;

    public PlayerSwitcher psScript;

    void Update()
    {
        if (sliderValue2[0] == 0 || sliderValue2[1] == 0 || sliderValue3[0] == 0 || sliderValue3[0] == 0)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }

        if ((!switchTurn) || (switchTurn && playerTwoDone == true))
        {
            playerOnePoints = int.Parse(textTotalPointsValue.text);
            playerOnePoints = (playerOnePoints - +totalPriceValue);
        }
        else
        {
            playerTwoPoints = int.Parse(textTotalPointsValue.text);
            playerTwoPoints = (playerTwoPoints - +totalPriceValue);
        }

        totalPriceValue = 0;
        for (int i = 0; i < sliderValue3.Length; i++)
        {
            totalPriceValue += (sliderValue3[i] * 3);
        }
        for (int i = 0; i < sliderValue2.Length; i++)
        {
            totalPriceValue += (sliderValue2[i] * 2);
        }
        textTotalPoints.text = totalPriceValue.ToString();

        if (playerOnePoints < 0)
        {
            next.interactable = false;
        }
        if (playerTwoPoints < 0)
        {
            next.interactable = false;
        }
    }

    public void ShowValue()
    {
        string sliderMessage = sliderUI[0].value.ToString();
        textSliderValue[0].text = sliderMessage;
        sliderValue3[0] = int.Parse(textSliderValue[0].text);
    }
    public void ShowValue2()
    {
        string sliderMessage1 = sliderUI[1].value.ToString();
        textSliderValue[1].text = sliderMessage1;
        sliderValue3[1] = int.Parse(textSliderValue[1].text);
    }
    public void ShowValue3()
    {
        string sliderMessage2 = sliderUI[2].value.ToString();
        textSliderValue[2].text = sliderMessage2;
        sliderValue2[0] = int.Parse(textSliderValue[2].text);
    }
    public void ShowValue4()
    {
        string sliderMessage3 = sliderUI[3].value.ToString();
        textSliderValue[3].text = sliderMessage3;
        sliderValue2[1] = int.Parse(textSliderValue[3].text);
    }

    public void NextButtonClick()
    {
        optionsMenu.SetActive(false);
        //for (int i = 0; i < groundCollider.Length; i++)
        //{
        //    groundCollider[i].enabled = true;
        //}
        PointsSaver();
    }

    public void SwitchPlayer()
    {
        if (playerOneDone == true && playerTwoDone == true)
        {
            optionsMenu.gameObject.SetActive(false);
            raycastOn = false;
            psScript.scriptOn = true;
            psScript.StartGame();
            return;
        }

        for (int i = 0; i < sliderUI.Length; i++)
        {
            sliderUI[i].value = 0;
        }
        if ((!switchTurn) || (switchTurn && playerTwoDone == true))
        {
            textTotalPointsValue.text = playerOnePoints.ToString();

            optionsMenu.gameObject.GetComponentInChildren<Image>().color = new Color32(45, 136, 221, 100);
            optionsMenu.gameObject.transform.GetChild(14).GetComponent<Image>().color = new Color32(45, 136, 221, 100);
            optionsMenu.gameObject.transform.GetChild(17).GetComponent<Image>().color = new Color32(45, 136, 221, 100);

            optionsMenu.SetActive(true);
            //for (int i = 0; i < groundCollider.Length; i++)
            //{
            //    groundCollider[i].enabled = false;
            //}
            return;
        }
        if (switchTurn || playerOneDone == true)
        {
            textTotalPointsValue.text = playerTwoPoints.ToString();

            optionsMenu.gameObject.GetComponentInChildren<Image>().color = new Color32(250, 56, 48, 100);
            optionsMenu.gameObject.transform.GetChild(14).GetComponent<Image>().color = new Color32(250, 56, 48, 100);
            optionsMenu.gameObject.transform.GetChild(17).GetComponent<Image>().color = new Color32(250, 56, 48, 100);

            optionsMenu.SetActive(true);
            //for (int i = 0; i < groundCollider.Length; i++)
            //{
            //    groundCollider[i].enabled = false;
            //}
        }
    }

    public void PointsSaver()
    {
        if (!switchTurn && !playerOneDone)
        {
            playerOnePoints = (playerOnePoints - totalPriceValue);
        }
        if (switchTurn && !playerTwoDone)
        {
            playerTwoPoints = (playerTwoPoints - totalPriceValue);
        }
    }
}
