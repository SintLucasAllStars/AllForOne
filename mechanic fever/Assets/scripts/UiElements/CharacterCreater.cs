using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreater : MonoBehaviour
{
    public int cost = 0;

    public GameObject blueCharacteBase;
    public GameObject redCharacterBase;

    public GameObject characterPreviewSpawnPoint;
    private GameObject characterPrefab;
    private characterEquipmentHandler prefabEquipmentHandler;

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

    private bool isCreatingCharacter;

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

        ResetCharacterCreater();
    }

    public void Update()
    {
        //TODO: refactor a more elegent solution
        if (TurnManager.turnManager.currentGameMode == TurnManager.GameMode.action && !isCreatingCharacter)
        {
            SetScreenActive(false);
            StopAllCoroutines();
            this.enabled = false;
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
                if (defenseSlider.value >= 8)
                {
                    prefabEquipmentHandler.EquipArmorLevel(1);
                }
                else if (defenseSlider.value > 3)
                {
                    prefabEquipmentHandler.UnEquipArmor();
                    prefabEquipmentHandler.EquipArmorLevel(0);
                }
                else
                {
                    prefabEquipmentHandler.UnEquipArmor();
                }
                break;
        }

        cost = (int)((healthSlider.value * 3) + (strengthSlider.value * 2) + (speedSlider.value * 3) + (defenseSlider.value * 2));
        costText.text = $"cost: {cost}";
    }

    public void CreateCharacter()
    {
        if (TurnManager.turnManager.BuyCharacter(cost))
        {
            isCreatingCharacter = true;
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
            characterPrefab.transform.position = rayHit.point;
            yield return new WaitForSeconds(.1f);
        }

        characterPrefab.GetComponent<CharacterController>().setStats(CreateStats());

        TurnManager.turnManager.EndTurn();

        yield return new WaitForSeconds(.1f);

        if (TurnManager.turnManager.currentGameMode == TurnManager.GameMode.setup)
        {
            ResetCharacterCreater();
        }
    }

    public void ResetCharacterCreater()
    {
        switch (TurnManager.turnManager.getTurnIndex())
        {
            case 1:
                characterPrefab = Instantiate(blueCharacteBase, characterPreviewSpawnPoint.transform.position, Quaternion.Euler(0, 180, 0), characterPreviewSpawnPoint.transform);
                break;
            case 2:
                characterPrefab = Instantiate(redCharacterBase, characterPreviewSpawnPoint.transform.position, Quaternion.Euler(0, 180, 0), characterPreviewSpawnPoint.transform);
                break;
        }

        prefabEquipmentHandler = characterPrefab.GetComponent<characterEquipmentHandler>();
        SetScreenActive(true);
        currencyText.text = $"current points: {TurnManager.turnManager.getCurrency()}";
        isCreatingCharacter = false;
    }

    public void turnChange()
    {
        StartCoroutine(turnTimer());
    }

    private IEnumerator turnTimer()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(characterPrefab);
        ResetCharacterCreater();
    }
}
