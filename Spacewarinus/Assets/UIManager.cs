using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour {
    //HiringUi
    [Header("HiringUi")]
    public GameObject[] Uiscreens;
    public Slider[] sliders;
    public Text[] sliderText;
    public Image teamImage;
    public Text pointsText;
    //UnitUi
    [Header("UnitUi")]
    public Text[] unitTexts;


    [HideInInspector]
    public TeamManager tm;
    [HideInInspector]
    public SelectionController sc;
    [HideInInspector]
    public bool isSpawn;
    private float cost;
    public Camera c;
	// Use this for initialization
	void Start () {
        tm = FindObjectOfType<TeamManager>();
        sc = FindObjectOfType<SelectionController>();
        ChangeTeamImage(0);
        SetSliderValues();

    }
	
	// Update is called once per frame
	void Update () {
        if (isSpawn)
        {
            SpawnRay();
        }
        if (sc.currentUnit != null && sc.currentUnit.isPlaying)
        {
            SetScreenActive(1, true);
            SetUnitUi();
        }
        else
        {
            SetScreenActive(1, false);
        }
	}

    public void SetScreenActive(int index,bool set)
    {
        Uiscreens[index].SetActive(set);
    }
    public void SetSliderValues()
    {
        cost = 0;
        float currentPoints = tm.players[tm.CurrentTeam].money;
        for (int i = 0; i < sliders.Length; i++)
        {
           sliderText[i].text = sliders[i].value.ToString();
            cost += sliders[i].value;
        }
        pointsText.text = "points : " + (currentPoints -= cost).ToString(); 

    }
    public void SetUnitUi()
    {
        Unit u = sc.currentUnit;

        unitTexts[0].text = u.health.ToString();
        unitTexts[1].text = u.strenght.ToString();
        unitTexts[2].text = u.defence.ToString();
        unitTexts[3].text = u.speed.ToString();
        unitTexts[4].text = ((int)(u.currentTime)).ToString();
    }
    public void SpawnRay()
    {
        RaycastHit hit;
        Ray ray = c.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray,out hit))
        {
            Transform hitTransform = hit.transform;
            if(hit.transform.tag != "OutSideGround")
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                tm.OnSpawn(GetStats(),hit.point);
                }
            }

        }
    }
    private float[] GetStats()
    {
        float[] s = new float[4];
        for (int i = 0; i < sliders.Length; i++)
        {
            s[i] = sliders[i].value;
        }
        return s;
    }
    public void OnHire()
    {
        if((tm.players[tm.CurrentTeam].money - cost) >= 0)
        {
        tm.players[tm.CurrentTeam].money -= (int)cost;
        SetScreenActive(0, false);
        isSpawn = true;

        }
        else
        {
            Debug.Log("no money");
        }
    }
    public void ChangeTeamImage(int index)
    {
        if(index == 0)
        {
            teamImage.color = Color.blue;
        }
        else
        {
            teamImage.color = Color.red;
        }
    }
    public void ResetSliders()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].value = 0;
        }
    }
    public bool CheckMoney()
    {
        if(tm.players[0].money <= 0 && tm.players[1].money <= 0)
        {
            return true;
        }
        return false;
    }
}
