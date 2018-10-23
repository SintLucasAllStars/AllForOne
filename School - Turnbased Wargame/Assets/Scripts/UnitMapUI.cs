using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMapUI : MonoBehaviour
{
    [SerializeField] GameObject unitIcon;
    [SerializeField] LayerMask layerUI;

    private List<GameObject> playerRedUI = new List<GameObject>();
    private List<GameObject> playerBlueUI = new List<GameObject>();

    private Camera mainCam;
    private bool checkClickUI = false;

    void OnEnable ()
    {
        if (gameObject.activeInHierarchy && playerRedUI.Count == 0 && playerBlueUI.Count == 0)
        {
            for (int i = 0; i < PlayerManager.instance.playerRed.playerGameObject.Count; i++)
            {
                GameObject p = PlayerManager.instance.playerRed.playerGameObject[i];

                GameObject unitUI = Instantiate(unitIcon, p.transform) as GameObject;
                unitUI.GetComponent<SpriteRenderer>().color = PlayerManager.instance.playerRed.playerUIColor;

                unitUI.GetComponent<UnitGameObjectInteractable>().unitGameObject = p;
                unitUI.GetComponent<UnitGameObjectInteractable>().unitIndex = i;

                playerRedUI.Add(unitUI);
            }

            for (int i = 0; i < PlayerManager.instance.playerBlue.playerGameObject.Count; i++)
            {
                GameObject p = PlayerManager.instance.playerBlue.playerGameObject[i];

                GameObject unitUI = Instantiate(unitIcon, p.transform) as GameObject;
                unitUI.GetComponent<SpriteRenderer>().color = PlayerManager.instance.playerBlue.playerUIColor;

                unitUI.GetComponent<UnitGameObjectInteractable>().unitGameObject = p;
                unitUI.GetComponent<UnitGameObjectInteractable>().unitIndex = i;

                playerBlueUI.Add(unitUI);
            }

            HideUnitUI();

            mainCam = GameControl.instance.camControl.GetComponent<Camera>();
        }
	}

    public void ShowUnitUI()
    {
        ShowUnitUI(true);
        ShowUnitUI(false);
    }

    public void ShowUnitUI (bool isPlayerblue)
    {
        foreach(GameObject o in isPlayerblue ? playerBlueUI : playerRedUI)
        {
            if (o != null)
            {
                o.SetActive(true);
            }
        }

        checkClickUI = true;
    }


    public void HideUnitUI()
    {
        HideUnitUI(true);
        HideUnitUI(false);
    }
    public void HideUnitUI(bool isPlayerblue)
    {
        foreach (GameObject o in isPlayerblue ? playerBlueUI : playerRedUI)
        {
            if (o != null)
                o.SetActive(false);
        
        }
        checkClickUI = false;
    }


    private void Update()
    {
        if (checkClickUI)
        {
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 50f, layerUI))   //5 = layer UI
            {
                if (Input.GetMouseButtonDown(0) && hit.collider.gameObject.GetComponent<UnitGameObjectInteractable>() != null)
                {
                    GameControl.instance.GameSelectUnit(PlayerManager.instance.playerCurrentTurn.
                        playerGameObject[hit.collider.gameObject.GetComponent<UnitGameObjectInteractable>().unitIndex]);

                    HideUnitUI();
                }
            }
        }
    }
}
