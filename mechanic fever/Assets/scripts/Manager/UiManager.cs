using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager uiManager;

    public GameObject warningScreen;
    public GameObject actionScreen;

    private GameObject[] actionScreens;
    private GameObject turnScreen;
    private Text turnText;
    private Text turnTimer;

    public Image fortifyBar;
    public Image attackBar;
    private void Awake()
    {
        if (uiManager is null)
        {
            uiManager = this;
        }
        else
        {
            Destroy(this);
        }

        actionScreens = new GameObject[actionScreen.transform.childCount];
        for (int i = 0; i < actionScreen.transform.childCount; i++)
        {
            actionScreens[i] = actionScreen.transform.GetChild(i).gameObject;
        }
    }

    public void controlStartMessage()
    {
        showMessage("Start", 0.5f);
    }

    public void LoadWarning()
    {
        warningScreen.SetActive(true);
        StartCoroutine(disableOverTime(warningScreen, 5));
    }

    public void showActionScreen(int playerIndex)
    {
        disableAllActionScreens();

        turnTimer = actionScreens[playerIndex].transform.GetChild(1).GetChild(0).GetComponent<Text>();
        turnScreen = actionScreens[playerIndex].transform.GetChild(2).gameObject;
        turnText = turnScreen.transform.GetChild(0).GetComponent<Text>();

        actionScreens[playerIndex].SetActive(true);

        showMessage($"player {playerIndex + 1} turn", 1);
    }

    public void disableAllActionScreens()
    {
        foreach (GameObject screen in actionScreens)
        {
            screen.SetActive(false);
        }
    }

    public void updateUnitControlTimer(float time)
    {
        turnTimer.text = time.ToString("00.0");
        if(time <= 0)
        {
            turnTimer.text = "";
        }
    }
    private IEnumerator disableOverTime(GameObject selectedObject, float time)
    {
        yield return new WaitForSeconds(time);
        selectedObject.SetActive(false);
    }

    public IEnumerator enableActionSceenOverTime(int playerIndex, float time)
    {
        yield return new WaitForSeconds(time);
        showActionScreen(playerIndex);
    }

    private void showMessage(string text, float time)
    {
        turnText.text = text;
        turnScreen.SetActive(true);

        StartCoroutine(disableOverTime(turnScreen, time));
    }

    public void updateFortified(float value, float baseTime)
    {
        fortifyBar.fillAmount = GameManager.Map(value, 0, baseTime, 0, 1);
    }

    public void updateAttackTimer(float value, float baseTime)
    {
        attackBar.fillAmount = GameManager.Map(value, 0, baseTime, 0, 1);
    }
}
