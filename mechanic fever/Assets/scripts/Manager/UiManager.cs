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

    private GameObject unitActionUi;
    private Image fortifyBar;
    private Image attackBar;

    private GameObject unitPowerUpUI;
    private Image powerUpImage;
    private Text powerUpText;
    public Sprite[] powerUpImages;

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

        unitActionUi = transform.GetChild(0).GetChild(2).gameObject;

        fortifyBar = unitActionUi.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        attackBar = unitActionUi.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();

        unitPowerUpUI = transform.GetChild(0).GetChild(3).gameObject;
        powerUpImage = unitPowerUpUI.transform.GetChild(0).GetComponent<Image>();
        powerUpText = unitPowerUpUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
    }

    #region character creation ui
    public void LoadWarning()
    {
        warningScreen.SetActive(true);
        StartCoroutine(disableOverTime(warningScreen, 5));
    }
    #endregion

    #region player border ui
    public void controlStartMessage()
    {
        showMessage("Start", 0.5f);
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
        if (turnTimer != null)
        {
            turnTimer.text = "";
        }

        foreach (GameObject screen in actionScreens)
        {
            screen.SetActive(false);
        }
    }

    public void updateUnitControlTimer(float time)
    {
        turnTimer.text = time.ToString("00.0");
        if (time <= 0)
        {
            turnTimer.text = "";
        }
    }

    public IEnumerator enableActionSceenOverTime(int playerIndex, float time)
    {
        yield return new WaitForSeconds(time);
        showActionScreen(playerIndex);
    }

    public void showMessage(string text, float time)
    {
        turnText.text = text;
        turnScreen.SetActive(true);

        StartCoroutine(disableOverTime(turnScreen, time));
    }
    #endregion

    #region action UI
    public void showUnitActionUi()
    {
        unitActionUi.SetActive(true);
    }

    public void disableUnitActionUi()
    {
        unitActionUi.SetActive(false);
    }

    public void updateFortified(float value, float baseTime)
    {
        fortifyBar.fillAmount = GameManager.Map(value, 0, baseTime, 0, 1);
    }

    public void updateAttackTimer(float value, float baseTime)
    {
        attackBar.fillAmount = GameManager.Map(value, 0, baseTime, 0, 1);
    }
    #endregion

    #region powerUpUi
    public void showPowerUpUi()
    {
        unitPowerUpUI.SetActive(true);
    }

    public void cycleThroughPowerups(int powerUpIndex)
    {
        switch (powerUpIndex)
        {
            case 0:
                powerUpText.text = "adrenaline";
                break;
            case 1:
                powerUpText.text = "rage";
                break;
            case 2:
                powerUpText.text = "time Stop ";
                break;
            default:
                disablePowerUpUi();
                return;
        }

        powerUpImage.sprite = powerUpImages[powerUpIndex];
    }

    public void disablePowerUpUi()
    {
        unitPowerUpUI.SetActive(false);
    }
    #endregion

    private IEnumerator disableOverTime(GameObject selectedObject, float time)
    {
        yield return new WaitForSeconds(time);
        selectedObject.SetActive(false);
    }
}
