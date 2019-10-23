using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class UIController : Singleton<UIController>
{

    private int strength_i, speed_i, health_i, defense_i;

    [Space]
    [Header("DropdownWeaponSelection")]
    [SerializeField] private TMP_Dropdown weaponSelection;

    [Space]
    [Header("CountDownTimer")]

    public TextMeshProUGUI countDownText;
    public TextMeshProUGUI currentPlayer;

    [SerializeField] private RectTransform BuyRoot;
    [SerializeField] private GameObject gamePhaseCanvas;
    private bool isBuyScreenShowing = false;

    private void Start()
    {
        AssignDropDown();
    }

    private void AssignDropDown()
    {
        for (int i = 0; i < Enum.GetNames(typeof(Weapon.TypeOfWeapon)).Length; i++)
        {
            var weaponName = (Weapon.TypeOfWeapon)i;
            weaponSelection.options.Add(new TMP_Dropdown.OptionData()
            {
                text = weaponName.ToString()
            });
        }
    }

    public void BuyUnit()
    {
        strength_i = UnityEngine.Random.Range(0, 10);
        speed_i = UnityEngine.Random.Range(0, 10);
        health_i = UnityEngine.Random.Range(0, 10);
        defense_i = UnityEngine.Random.Range(0, 10);

        Debug.Log("Stats of the Unit\n" + "strength: " + strength_i + "\nspeed: " + speed_i + "\nhealth: " + health_i + "\ndefense: " + defense_i);

        var enumValue = (Weapon.TypeOfWeapon)weaponSelection.value;
        Unit _unit = new Unit(health_i, strength_i, speed_i, defense_i, enumValue);
        GameController.instance.BuyUnit(_unit, health_i, strength_i, speed_i, defense_i);
    }

    public void CountDownTimer(double _timer) => countDownText.text = _timer.ToString();

    public void ShowBuyScreen()
    {
        float speed = 0.5f;
        int[] pos = new int[2] { 0, -480 };

        if (isBuyScreenShowing)
        {
            LeanTween.moveY(BuyRoot, -pos[1], speed);
        }
        else if (!isBuyScreenShowing)
        {
            LeanTween.moveY(BuyRoot, -pos[0], speed);
        }
        isBuyScreenShowing = !isBuyScreenShowing;

    }
    //
    public void GamePhase()
    {
        ShowBuyScreen();
        gamePhaseCanvas.SetActive(true);
        SetCurrentPlayerText();
    }

    public void SetCurrentPlayerText()
    {
        if (GameController.isCurrentPlayerOne)
        {
            currentPlayer.text = "Player 1 turn";
        }
        else
        {
            currentPlayer.text = "Player 2 turn";
        }
    }
}
