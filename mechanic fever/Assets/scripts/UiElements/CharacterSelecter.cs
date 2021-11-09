using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelecter : MonoBehaviour
{
    public Vector3 cameraOffset;
    public Vector3 rotationOffset;
    private Quaternion oldRotation;
    private Vector3 oldPosition;

    private RaycastHit rayHit;

    public Transform cameraBase;
    public GameObject selectionPanel;
    [HideInInspector]
    public GameObject selectedCharacter;

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


        GameManager.gameManager.setCharacterSelector(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.currentGameMode == GameManager.GameMode.action && !GameManager.gameManager.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && GameManager.gameManager.controllingCamera)
            {
                Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out rayHit);

                if (rayHit.collider.CompareTag($"player{GameManager.gameManager.getTurnIndex()}Owned"))
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

    private void selectCharacter()
    {
        selectedCharacter.GetComponent<CharacterController>().startCharacterControl();
    }

    #region cameraMovement
    private IEnumerator PositionCamera()
    {
        GameManager.gameManager.controllingCamera = false;

        oldRotation = transform.rotation;
        oldPosition = transform.position;

        Vector3 offset = (cameraOffset.y * selectedCharacter.transform.up) + (cameraOffset.z * selectedCharacter.transform.forward);

        while (transform.position != selectedCharacter.transform.position + offset && transform.rotation != selectedCharacter.transform.rotation)
        {
            transform.position = Vector3.Lerp(transform.position, selectedCharacter.transform.position + offset, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, selectedCharacter.transform.rotation * Quaternion.Euler(rotationOffset), 0.5f);
            yield return new WaitForSeconds(0.1f);
        }

        transform.parent = selectedCharacter.transform;

        selectCharacter();
    }

    public IEnumerator resetCamera()
    {
        yield return new WaitForSeconds(2f);

        transform.parent = null;

        while (transform.position != oldPosition && transform.rotation != oldRotation)
        {
            transform.position = Vector3.Lerp(transform.position, oldPosition, 0.5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, oldRotation, 0.5f);
            yield return new WaitForSeconds(0.1f);
        }

        transform.parent = cameraBase;
        GameManager.gameManager.controllingCamera = true;
    }
    #endregion

    #region ui Elements

    public void TakeControl()
    {
        closeUiWindow();
        StartCoroutine(PositionCamera());
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
