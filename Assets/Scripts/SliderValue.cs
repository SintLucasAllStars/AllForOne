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

    int whichAvatarIsOn = 1;

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
        }

        playerOnePoints = 0;
        for (int i = 0; i < sliderValue3.Length; i++)
        {
            playerOnePoints += (-sliderValue3[i] * 3 + 50);
        }
        for (int i = 0; i < sliderValue2.Length; i++)
        {
            playerOnePoints += (-sliderValue2[i] * 2);
        }
        textTotalPoints.text = playerOnePoints.ToString();
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

    //public void NextButtonClick()
    //{
    //    for (int i = 0; i < sliderUI.Length; i++)
    //    {
    //        sliderUI[i].value = 0;
    //    }

    //    //switchTurn = !switchTurn;

    //    if (!switchTurn)
    //    {
    //        playerOnePoints = totalPointsValue;
    //        optionsMenu.gameObject.GetComponentInChildren<Image>().color = new Color32(45, 136, 221, 100);
    //        optionsMenu.gameObject.transform.GetChild(14).GetComponent<Image>().color = new Color32(45, 136, 221, 100);
    //        optionsMenu.SetActive(false);
    //        groundCollider.enabled = true;
    //    }
    //    else
    //    {
    //        totalPointsValue = playerTwoPoints;
    //        textTotalPoints.text = totalPointsValue.ToString();
    //        optionsMenu.SetActive(true);
    //        optionsMenu.gameObject.GetComponentInChildren<Image>().color = new Color32(250, 56, 48, 100);
    //        optionsMenu.gameObject.transform.GetChild(14).GetComponent<Image>().color = new Color32(250, 56, 48, 100);
    //        groundCollider.enabled = false;
    //        totalPointsValue = playerTwoPoints;
    //        playerTwoPoints = totalPointsValue;
    //        switchTurn = !switchTurn;
    //    }
    //}

    public void SwitchAvatar()
    {
        for (int i = 0; i < sliderUI.Length; i++)
        {
            sliderUI[i].value = 0;
        }

        // processing whichAvatarIsOn variable
        switch (whichAvatarIsOn)
        {

            // if the first avatar is on
            case 1:

                Debug.Log("1");
                // then the second avatar is on now
                whichAvatarIsOn = 2;

                groundCollider.enabled = true;
                playerOnePoints = totalPointsValue;
                textTotalPoints.text = totalPointsValue.ToString();
                optionsMenu.gameObject.SetActive(false);
                break;

            // if the second avatar is on
            case 2:

                Debug.Log("2");
                // then the first avatar is on now
                whichAvatarIsOn = 3;

                groundCollider.enabled = false;
                playerTwoPoints = totalPointsValue;
                textTotalPoints.text = totalPointsValue.ToString();
                optionsMenu.gameObject.SetActive(true);
                break;

            case 3:

                Debug.Log("3");
                // then the first avatar is on now
                whichAvatarIsOn = 4;

                groundCollider.enabled = true;
                playerTwoPoints = totalPointsValue;
                textTotalPoints.text = totalPointsValue.ToString();
                optionsMenu.gameObject.SetActive(false);
                break;

            case 4:

                Debug.Log("4");
                // then the first avatar is on now
                whichAvatarIsOn = 1;

                groundCollider.enabled = false;
                Debug.Log(playerOnePoints);
                textTotalPoints.text = playerOnePoints.ToString();
                optionsMenu.gameObject.SetActive(true);
                break;
        }

    }
}
