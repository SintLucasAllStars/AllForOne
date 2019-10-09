using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SliderValue : MonoBehaviour
{
    public Slider[] sliderUI;
    public TMP_Text[] textSliderValue;
    public TMP_Text textTotalPoints;

    public int[] sliderValue3;
    public int[] sliderValue2;
    public int totalPointsValue = 100;

    public Button next;

    public GameObject optionsMenu;

    public int playerOnePoints, playerTwoPoints = 100;

    public bool switchTurn;

    public MeshCollider groundCollider;

    void Start()
    {

    }

    void Update()
    {
        if(sliderValue2[0] == 0 || sliderValue2[1] == 0 || sliderValue3[0] == 0 || sliderValue3[0] == 0)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
            totalPointsValue = 0;
            for (int i = 0; i < sliderValue3.Length; i++)
            {
                totalPointsValue += (-sliderValue3[i] * 3 + 50);
            }
            for (int i = 0; i < sliderValue2.Length; i++)
            {
                totalPointsValue += (-sliderValue2[i] * 2);
            }
            textTotalPoints.text = totalPointsValue.ToString();
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
        for (int i = 0; i < sliderUI.Length; i++)
        {
            sliderUI[i].value = 0;
        }

        //switchTurn = !switchTurn;

        if (!switchTurn)
        {
            playerOnePoints = totalPointsValue;
            optionsMenu.gameObject.GetComponentInChildren<Image>().color = new Color32(45, 136, 221, 100);
            optionsMenu.gameObject.transform.GetChild(14).GetComponent<Image>().color = new Color32(45, 136, 221, 100);
            optionsMenu.SetActive(false);
            groundCollider.enabled = true;
        }
        else
        {
            totalPointsValue = playerTwoPoints;
            textTotalPoints.text = totalPointsValue.ToString();
            optionsMenu.SetActive(true);
            optionsMenu.gameObject.GetComponentInChildren<Image>().color = new Color32(250, 56, 48, 100);
            optionsMenu.gameObject.transform.GetChild(14).GetComponent<Image>().color = new Color32(250, 56, 48, 100);
            groundCollider.enabled = false;
            totalPointsValue = playerTwoPoints;
            playerTwoPoints = totalPointsValue;
            switchTurn = !switchTurn;
        }
    }
}
