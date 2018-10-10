using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitUIEvent : MonoBehaviour
{
    public UnitPlacement placementUnit;
    public List<SoldierAsset> standardUnit;

    void Start()
    {
        CreateCanvasList();
        customUnitCreate.SetActive(false);
        NavigaTo(CanvasNavigation.unitList);
    }


    #region CanvasUI

    public enum CanvasNavigation { none, unitList, customUnit };
    [SerializeField] private CanvasNavigation m_canvasNaviga;
    public CanvasNavigation canvasNaviga
    {
        get
        {
            return m_canvasNaviga;
        }
        set
        {
            CanvasNavigation old = m_canvasNaviga;
            m_canvasNaviga = value;
            OnChangedCanvasNaviga(old);
        }
    }


    public GameObject CanvasUnit;

    public GameObject unitListScroll;
    public GameObject customUnitCreate;
    public Text titleText;

    public GameObject unitUI;

    public void CreateCanvasList ()
    {
        GameObject scrollContent = unitListScroll.GetComponentsInChildren<Transform>()[1].gameObject;

        for(int i = 0; i < standardUnit.Count; i++)
        {
            GameObject ui = Instantiate(unitUI, scrollContent.transform) as GameObject;
            ui.transform.Translate(400 * i, 0, 0);

            foreach(Text t in ui.GetComponentsInChildren<Text>())
            {
                switch(t.text.ToLower())
                {
                    case "title":
                        t.text = standardUnit[i].unitSoldier.name;
                        break;

                    case "cost":
                        t.text = "$" + standardUnit[i].unitSoldier.cost;
                        break;
                }
            }

            int c = i + 1;
            ui.GetComponentInChildren<Button>().onClick.AddListener(() => OnUnitListClick(c));

        }
    }

    public void NavigaTo(CanvasNavigation to)
    {
        canvasNaviga = to;
    }

    public void OnCustomUnitClick (UnitUICustomStats customUnit)
    {
        SoldierAsset sa = customUnit.CreateAsset();
        if (PlayerManager.instance.playerRed.playerMoney >= sa.unitSoldier.cost)
        {
            NavigaTo(CanvasNavigation.none);
            placementUnit.selectSoldier = sa;
        }
    }

    public void OnUnitListClick(int index)
    {
        if (index == 0)
        {
            NavigaTo(CanvasNavigation.customUnit);
        }
        else if (PlayerManager.instance.playerRed.playerMoney >= standardUnit[index - 1].unitSoldier.cost)
        {
            NavigaTo(CanvasNavigation.none);
            placementUnit.selectSoldier = standardUnit[index - 1];
        }
    }

    public void OnChangedCanvasNaviga(CanvasNavigation oldCanvas)
    {
        //Set current canvas to hide
        OnDrawCanvasNaviga(oldCanvas, false);
        //Set next canvas to show
        OnDrawCanvasNaviga(m_canvasNaviga, true);
    }

    private void OnDrawCanvasNaviga(CanvasNavigation to, bool enable)
    {
        switch (to)
        {

            case CanvasNavigation.unitList:
                unitListScroll.SetActive(enable);
                if (enable)
                {
                    CanvasUnit.SetActive(true);
                    titleText.text = "Select a unit";
                }
                break;

            case CanvasNavigation.customUnit:
                customUnitCreate.SetActive(enable);
                if (enable)
                {
                    CanvasUnit.SetActive(true);
                    titleText.text = "Create a unit";
                }
                break;

            case CanvasNavigation.none:

                if (enable)
                {
                    CanvasUnit.SetActive(false);
                }
                break;
        }
    }

    #endregion
}
