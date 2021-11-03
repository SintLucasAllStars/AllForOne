using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelecter : MonoBehaviour
{
    public Vector3 chameraOffset;
    private Quaternion oldRotation;
    private Vector3 oldPosition;

    private RaycastHit rayHit;

    public Transform cameraBase;
    public GameObject selectionPanel;
    private GameObject selectedCharacter;

    private Slider healthSlider;
    private Slider strengthSlider;
    private Slider speedSlider;
    private Slider defenseSlider;
    private Text healthText;
    private Text strengthText;
    private Text speedText;
    private Text defenseText;

    private void Start()
    {
        healthSlider = selectionPanel.transform.GetChild(0).GetChild(0).GetComponent<Slider>();
        strengthSlider = selectionPanel.transform.GetChild(0).GetChild(1).GetComponent<Slider>();
        speedSlider = selectionPanel.transform.GetChild(0).GetChild(2).GetComponent<Slider>();
        defenseSlider = selectionPanel.transform.GetChild(0).GetChild(3).GetComponent<Slider>();
        healthText = healthSlider.transform.GetChild(0).GetComponent<Text>();
        strengthText = strengthSlider.transform.GetChild(0).GetComponent<Text>();
        speedText = speedSlider.transform.GetChild(0).GetComponent<Text>();
        defenseText = defenseSlider.transform.GetChild(0).GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TurnManager.turnManager.currentGameMode == TurnManager.GameMode.action)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && TurnManager.turnManager.controllingCamera)
            {
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit);

                if (rayHit.collider.CompareTag($"{TurnManager.turnManager.GetCurrentTurn()}Owned"))
                {
                    selectedCharacter = rayHit.collider.gameObject;
                    openUiWindow();
                }
                else if (!EventSystem.current.IsPointerOverGameObject())
                {
                    closeUiWindow();
                }
            }
        }
    }

    private void selectCharacter(GameObject character)
    {
        character.GetComponent<CharacterController>().startCharacterControl(this);
    }

    #region cameraMovement
    private IEnumerator PositionCamera(GameObject character)
    {
        TurnManager.turnManager.controllingCamera = false;

        oldRotation = transform.rotation;
        oldPosition = transform.position;

        while (transform.position != character.transform.position + chameraOffset && transform.rotation != character.transform.rotation)
        {
            transform.position = Vector3.Lerp(transform.position, character.transform.position + chameraOffset, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, character.transform.rotation, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }

        transform.parent = character.transform;

        selectCharacter(character);
    }

    public IEnumerator resetCamera()
    {
        transform.parent = null;

        while (transform.position != oldPosition && transform.rotation != oldRotation)
        {
            transform.position = Vector3.Lerp(transform.position, oldPosition, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, oldRotation, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }

        transform.parent = cameraBase;
        TurnManager.turnManager.controllingCamera = true;
    }
    #endregion

    #region ui Elements

    public void TakeControl()
    {
        closeUiWindow();
        StartCoroutine(PositionCamera(selectedCharacter));
    }

    private void openUiWindow()
    {
        healthSlider.value = selectedCharacter.GetComponent<CharacterController>().Health;
        strengthSlider.value = selectedCharacter.GetComponent<CharacterController>().Strength;
        speedSlider.value = selectedCharacter.GetComponent<CharacterController>().Speed;
        defenseSlider.value = selectedCharacter.GetComponent<CharacterController>().Defense;
        healthText.text = $"Health: {healthSlider.value}";
        strengthText.text = $"strength: {strengthSlider.value}";
        speedText.text = $"Speed: {speedSlider.value}";
        defenseText.text = $"Defense: {defenseSlider.value}";

        selectionPanel.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(selectedCharacter.transform.position) + new Vector3(100, 100, 0);
        selectionPanel.SetActive(true);
    }

    private void closeUiWindow()
    {
        selectionPanel.SetActive(false);
    }
    #endregion
}
