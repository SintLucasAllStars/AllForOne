using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCreater : MonoBehaviour
{
    [HideInInspector]
    public int cost = 0;

    public GameObject[] CharacterBases;

    public GameObject characterPreviewSpawnPoint;
    private GameObject characterPrefab;
    private characterEquipmentHandler prefabEquipmentHandler;

    private float healthValue;
    private float strenghtValue;
    private float speedValue;
    private float defenseValue;

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

        CalculatePrice();

        ResetCharacterCreater();
        equipmentManagment();
    }

    public void Update()
    {
        if (GameManager.gameManager.currentGameMode == GameManager.GameMode.action && !isCreatingCharacter)
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
                equipmentManagment();
                break;
        }

        CalculatePrice();
    }

    private void CalculatePrice()
    {
        healthValue = GameManager.Map(healthSlider.value, 1, 100, 3, 30);
        strenghtValue = GameManager.Map(strengthSlider.value, 1, 100, 2, 20);
        speedValue = GameManager.Map(speedSlider.value, 1, 100, 3, 30);
        defenseValue = GameManager.Map(defenseSlider.value, 1, 100, 2, 20);

        cost = (int)(healthValue + strenghtValue + speedValue + defenseValue);
        costText.text = $"cost: {cost}";
    }

    public void CreateCharacter()
    {
        if (GameManager.gameManager.GetPlayer().BuyCharacter(cost))
        {
            isCreatingCharacter = true;
            SetScreenActive(false);
            StartCoroutine(PlaceCharacter());
        }
        else
        {
            UiManager.uiManager.LoadWarning();
        }
    }

    public CharacterStats CreateStats()
    {
        healthValue = healthSlider.value;
        strenghtValue = GameManager.Map(strengthSlider.value, 1, 100, 1, 10);
        speedValue = GameManager.Map(speedSlider.value, 1, 100, 1, 10);
        defenseValue = GameManager.Map(defenseSlider.value, 1, 100, 1, 10);

        return new CharacterStats(healthValue, strenghtValue, speedValue, defenseValue, GameManager.gameManager.getTurnIndex());
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
            Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit, 100, ~(1 << 6));
            characterPrefab.transform.position = rayHit.point;
            yield return new WaitForSeconds(.1f);
        }

        characterPrefab.GetComponent<CharacterController>().setStats(CreateStats());
        characterPrefab.GetComponent<CapsuleCollider>().enabled = true;
        characterPrefab.GetComponent<Rigidbody>().useGravity = true;

        if (GameManager.gameManager.currentGameMode == GameManager.GameMode.setup)
        {
            GameManager.gameManager.EndTurn();

            yield return new WaitForSeconds(.1f);

            ResetCharacterCreater();
        }

        //TODO: end setup fase banner
    }

    public void ResetCharacterCreater()
    {
        characterPrefab = 
            Instantiate(CharacterBases[GameManager.gameManager.getTurnIndex()], characterPreviewSpawnPoint.transform.position, Quaternion.Euler(0, 180, 0), characterPreviewSpawnPoint.transform);

        prefabEquipmentHandler = characterPrefab.GetComponent<characterEquipmentHandler>();
        SetScreenActive(true);
        currencyText.text = $"current points: {GameManager.gameManager.GetPlayer().getCurrency()}";

        healthSlider.value = Random.Range(1, 101);
        strengthSlider.value = Random.Range(1, 101);
        speedSlider.value = Random.Range(1, 101);
        defenseSlider.value = Random.Range(1, 101);

        isCreatingCharacter = false;
    }

    private void equipmentManagment()
    {
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
    }

    public void turnChange()
    {
        GameManager.gameManager.PlayerDoneSetupFase();
        StartCoroutine(turnTimer());
    }

    private IEnumerator turnTimer()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(characterPrefab);
        ResetCharacterCreater();
    }
}
