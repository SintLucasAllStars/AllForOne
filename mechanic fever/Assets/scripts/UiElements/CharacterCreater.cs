using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreater : MonoBehaviour
{
    public int cost = 0;

    public GameObject baseCharacter;
    public GameObject characterPreviewSpawnPoint;
    private GameObject characterPrefab;

    public Slider healthSlider;
    public Slider strengthSlider;
    public Slider speedSlider;
    public Slider defenseSlider;

    private GameObject panel;

    private Text currencyText;
    private Text costText;

    private Text healthText;
    private Text strengthText;
    private Text speedText;
    private Text defenseText;

    private void Start()
    {
        panel = transform.GetChild(0).gameObject;
        costText = panel.transform.GetChild(0).GetComponent<Text>();
        currencyText = panel.transform.GetChild(1).GetComponent<Text>();

        healthText = healthSlider.transform.GetChild(0).GetComponent<Text>();
        strengthText = strengthSlider.transform.GetChild(0).GetComponent<Text>();
        speedText = speedSlider.transform.GetChild(0).GetComponent<Text>();
        defenseText = defenseSlider.transform.GetChild(0).GetComponent<Text>();

        cost = (int)((healthSlider.value * 3) + (strengthSlider.value * 2) + (speedSlider.value * 3) + (defenseSlider.value * 2));
        costText.text = $"cost: {cost}";

        characterPrefab = Instantiate(baseCharacter, characterPreviewSpawnPoint.transform.position, Quaternion.identity, characterPreviewSpawnPoint.transform);
    }

    public void Update()
    {
        //TODO: refactor a more elegent solution
        if (TurnManager.turnManager.currentGameMode == TurnManager.GameMode.action)
        {
            SetScreenActive(false);
            StopAllCoroutines();
        }
    }

    public void ValueChange(int index)
    {
        switch (index)
        {
            case 0:
                healthText.text = $"Health: {healthSlider.value}";
                break;
            case 1:
                strengthText.text = $"Strength: {strengthSlider.value}";
                break;
            case 2:
                speedText.text = $"Speed: {speedSlider.value}";
                break;
            case 3:
                defenseText.text = $"Defense: {defenseSlider.value}";
                break;
        }

        cost = (int)((healthSlider.value * 3) + (strengthSlider.value * 2) + (speedSlider.value * 3) + (defenseSlider.value * 2));
        costText.text = $"cost: {cost}";
    }

    public void CreateCharacter()
    {
        if (TurnManager.turnManager.BuyCharacter(cost))
        {
            SetScreenActive(false);
            StartCoroutine(PlaceCharacter());
        }
    }

    public CharacterStats CreateStats()
    {
        return new CharacterStats((int)healthSlider.value, (int)strengthSlider.value, (int)speedSlider.value, (int)defenseSlider.value);
    }

    public void SetScreenActive(bool value)
    {
        panel.SetActive(value);
    }

    private IEnumerator PlaceCharacter()
    {
        RaycastHit rayHit;
        characterPrefab.transform.parent = null;

        while (!Input.GetKey(KeyCode.Mouse0))
        {
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit);
            characterPrefab.transform.position = rayHit.point + (Vector3.up);
            yield return new WaitForSeconds(.1f);
        }

        TurnManager.turnManager.EndTurn();

        yield return new WaitForSeconds(.1f);

        if (TurnManager.turnManager.currentGameMode == TurnManager.GameMode.setup)
        {
            ResetCharacterCreater();
        }
    }

    public void ResetCharacterCreater()
    {
        characterPrefab = Instantiate(baseCharacter, characterPreviewSpawnPoint.transform.position, Quaternion.identity, characterPreviewSpawnPoint.transform);
        SetScreenActive(true);
        currencyText.text = $"current points: {TurnManager.turnManager.getCurrency()}";
    }
}
