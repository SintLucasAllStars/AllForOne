using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class UiManager : MonoBehaviour
{
    public enum UiGroups
    {
        CreationUi = 0
    }
    
    
    //CreationUi  Value holders
    private int selectedClass = -1; // -1 = no class
    private string charName = "name";
    private int uiCharHp = 1; 
    private int uiCharStr = 1;
    private int uiCharSpd = 1;
    private int uiCharDef = 1;
    private List<int> uiCharStats = new List<int>();
    private int uiPoints;
   
    private int maxPoints;
    private int pointCap;
    public int minimumStatPercentage = 1;
    public int maxPointsPerStat = 50;
    private int amountOfStats = 4;
    // 100 divided by the values entered below
    public float hpPortion = 3f;
    public float strengthPortion = 6f;
    public float speedPortion = 3f;
    public float defensePortion = 6f;
    
    float hpCost;
    float strCost;
    float spdCost;
    float defCost;
    public List<float> costList = new List<float>();
    
    private GameManager gm;
    // ui overlay groups
    public List<GameObject> ui;
    public GameObject creationUi;
    public List<GameObject> creationUiText = new List<GameObject>();

    public List<GameObject> charCreateUi = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        uiCharStats.Add(uiCharHp);
        uiCharStats.Add(uiCharStr);
        uiCharStats.Add(uiCharSpd);
        uiCharStats.Add(uiCharDef);
        
        
        
        if (SceneManager.GetActiveScene().name == "Main Level")
        {
            if (creationUi == null)
            {
                creationUi = GameObject.Find("Character Creation");
            }
        }
        gm = GameObject.Find("Managers").GetComponent<GameManager>();
    }

    public void ActivateUi(UiGroups targetUi)
    {
        if (targetUi == UiGroups.CreationUi)
        {
            // for display and calculations. after phase end update turnHolderPoints
            uiPoints = gm.turnHolderPoints;
            
            CreationDefaults(gm.maxCreationPoints);
            RandomizeValues(gm.turnHolderPoints, minimumStatPercentage, amountOfStats);
            
            creationUi.SetActive(true);
            UiUpdate(UiGroups.CreationUi);
        }
    }

    public void RandomizeValues(float playerPoints, int minimumPercent, int numberOfStats)
    {
        pointCap = Mathf.RoundToInt(playerPoints);
        float statCost;
        float totalcost = 0;
        float midRangeModif = maxPointsPerStat;
        midRangeModif = midRangeModif / 3;

        for (int i = 0; i < uiCharStats.Count; i++)
        {
            float maxAllStats =  maxPointsPerStat * numberOfStats;
            float minimumAssign = maxAllStats / 100;
            float randomAssign;
          
            randomAssign = UnityEngine.Random.Range(minimumAssign, maxPointsPerStat);
            if (pointCap < randomAssign)
            {
                randomAssign = pointCap;
            }
            
            uiCharStats[i] = Mathf.RoundToInt(randomAssign);
            statCost =  costList[i];
            statCost = statCost * randomAssign;
            pointCap = pointCap - Mathf.RoundToInt(statCost);
            totalcost = totalcost + statCost;
        }

        
        
        Debug.Log(totalcost);
        
        
        uiCharHp = uiCharStats[0];
        uiCharStr = uiCharStats[1];
        uiCharSpd = uiCharStats[2];
        uiCharDef = uiCharStats[3];
        uiPoints = pointCap;
    }

    public void UiUpdate(UiGroups targetUi)
    {
        if (targetUi == UiGroups.CreationUi)
        {
            charCreateUi[0].GetComponent<TextMeshProUGUI>().text = charName;
            
            charCreateUi[1].GetComponent<TextMeshProUGUI>().text = uiCharHp.ToString();
            charCreateUi[2].GetComponent<TextMeshProUGUI>().text = uiCharStr.ToString();
            charCreateUi[3].GetComponent<TextMeshProUGUI>().text = uiCharSpd.ToString();
            charCreateUi[4].GetComponent<TextMeshProUGUI>().text = uiCharDef.ToString();

            charCreateUi[5].GetComponent<TextMeshProUGUI>().text = uiPoints.ToString();
        }
    }

    public void CreationDefaults(int playerMaxPoints)
    {
        
        maxPoints = playerMaxPoints;
        
        // turn the values to the portions of 100(if all added up result should be 100)
        hpPortion = maxPoints / hpPortion;
        strengthPortion = maxPoints / strengthPortion;
        speedPortion = maxPoints / speedPortion;
        defensePortion = maxPoints / defensePortion;
        
        //stat point cost calculation
        hpCost = hpPortion / maxPointsPerStat;
        strCost = strengthPortion / maxPointsPerStat;
        spdCost = speedPortion / maxPointsPerStat;
        defCost = defensePortion / maxPointsPerStat;
        costList.Clear();
        
        costList.Add(hpCost);
        costList.Add(strCost);
        costList.Add(spdCost);
        costList.Add(defCost);
    }

   

    public void PortionCalculation(float stat1Portion, float stat2Portion, float stat3PPortion, float stat4Portion)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private StatList CreateStatList()
    {
        string team = gm.turnHolder;
        
        //
        string name = charName;
        int Hp = uiCharHp;
        int Str = uiCharStr;
        int Spd = uiCharSpd;
        int Def = uiCharDef;
        StatList statInstance = new StatList(team, name, Hp, Str, Spd, Def);
        return statInstance;
    }

    //button
 

    public void CreatePlayerCharacter(int charClass)
    {
        StatList flatStats = CreateStatList();

        GameObject prefab = gm.charPrefabs[charClass];
        
        
        GameObject instance = Instantiate(prefab, Vector3.zero, Quaternion.identity);
        instance.GetComponent<PlayerCharacter>().stats = flatStats;

        gm.AddUnitToTeam(instance);
        StartCoroutine(gm.PhaseEnd(GameManager.Phase.PlacingUnits, GameManager.Phase.CreatePlayerCharacters));
    }

    public void SelectPrefab()
    {
        
    }
}
