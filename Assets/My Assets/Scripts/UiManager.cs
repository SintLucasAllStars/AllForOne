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
    public List<int> uiCharStats = new List<int>();
    public  float uiPoints;
    private string uiTurnHolder = "";
   
    private int maxPoints;
    public float pointCap;
    public float onePercentFactor = 0;
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
    //Preview and Dummy positions
    public Transform dummyPos;
    
    
    //character to place
    public GameObject charInstance;
    
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

    public void ActivateUi(UiGroups targetUi, bool activate)
    {
        if (targetUi == UiGroups.CreationUi)
        {
            // for display and calculations. after phase end update turnHolderPoints
            uiPoints = gm.turnHolderPoints;
            
            CreationDefaults(gm.maxCreationPoints);
            RandomizeValues(uiPoints, onePercentFactor, amountOfStats);
            
            creationUi.SetActive(activate);
            UiUpdate(UiGroups.CreationUi);
        }
    }

    public void RandomizeValues(float playerPoints, float minimumPercent, int numberOfStats)
    {
        pointCap = Mathf.RoundToInt(playerPoints);
        Debug.Log(uiPoints+"before");
        float statCost;
        float totalcost = 0;
        float midRangeModif = maxPointsPerStat;
        midRangeModif = midRangeModif / 3 * 2;

        float minPoints = maxPointsPerStat * numberOfStats;
        /*minPoints = minPoints / 100;
        minPoints =*/

        if (pointCap >= midRangeModif * numberOfStats)
        {
            
            for (int i = 0; i < uiCharStats.Count; i++)
            {
                float maxAllStats =  maxPointsPerStat * numberOfStats;
                float minimumAssign = maxAllStats / 100;
                float randomAssign;
          
                randomAssign = UnityEngine.Random.Range(minimumAssign, maxPointsPerStat);
                /*if (pointCap < randomAssign)
                {
                    randomAssign = pointCap;
                }*/
            
                uiCharStats[i] = Mathf.RoundToInt(randomAssign);
                statCost =  costList[i];
                statCost = statCost * randomAssign;
                pointCap = pointCap - statCost;
                totalcost = totalcost + statCost;
            }
        }
        else
        {
            for (int i = 0; i < uiCharStats.Count; i++)
            {
                float randomAssign;
          
                randomAssign = UnityEngine.Random.Range(midRangeModif / 2, midRangeModif);
                
                uiCharStats[i] = Mathf.RoundToInt(randomAssign);
                statCost =  costList[i];
                statCost = statCost * randomAssign;
                pointCap = pointCap - statCost;
                totalcost = totalcost + statCost;
            }
        }
        

        
        
        Debug.Log(uiPoints+"after");
        
        
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
            
            charCreateUi[1].GetComponent<TextMeshProUGUI>().text = "Health: " + uiCharHp.ToString();
            charCreateUi[2].GetComponent<TextMeshProUGUI>().text = "Strength: " + uiCharStr.ToString();
            charCreateUi[3].GetComponent<TextMeshProUGUI>().text = "Speed: " + uiCharSpd.ToString();
            charCreateUi[4].GetComponent<TextMeshProUGUI>().text = "Defense: " + uiCharDef.ToString();

            charCreateUi[5].GetComponent<TextMeshProUGUI>().text = "Creation points: " + Math.Round(uiPoints,2).ToString();
            if (gm.turnHolder == "Red")
            {
                charCreateUi[6].GetComponent<TextMeshProUGUI>().text = "Your turn player 1";
            }
            else
            {
                charCreateUi[6].GetComponent<TextMeshProUGUI>().text = "Your turn player 2";
            }
            
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
        
        //minimumFactor
        onePercentFactor = onePercentFactor + hpPortion / maxPoints;
        onePercentFactor = onePercentFactor + strengthPortion / maxPoints;
        onePercentFactor = onePercentFactor + speedPortion / maxPoints;
        onePercentFactor = onePercentFactor + defensePortion / maxPoints;

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
        
        // this should equal to 1% of maxPoints
        Debug.Log(onePercentFactor);
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
        charName = charCreateUi[0].GetComponent<TextMeshProUGUI>().text; 
        StatList flatStats = CreateStatList();

        GameObject prefab = gm.charPrefabs[charClass];
        
        
        charInstance = Instantiate(prefab, dummyPos.position, Quaternion.identity);
        charInstance.GetComponent<PlayerCharacter>().stats = flatStats;

        gm.AddUnitToTeam(charInstance);
        gm.characterInstance = charInstance;
        StartCoroutine(gm.PhaseEnd(GameManager.Phase.PlacingUnits, GameManager.Phase.CreatePlayerCharacters));
        if (gm.turnHolder == "Red")
        {
            gm.redPlayerCreationPoints = uiPoints;
        }
        else
        {
            gm.bluePlayerCreationPoints = uiPoints;
        }
    }

    public void AddStat(int statId)
    {
        // hp
        if (statId == 0)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharHp < maxPointsPerStat)
                {
                    uiCharHp++;
                    uiPoints--;
                    uiPoints = uiPoints - costList[statId];
                }
            }
        }

        //Str
        if (statId == 1)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharStr < maxPointsPerStat)
                {
                    uiCharStr++;
                    uiPoints--;
                    uiPoints = uiPoints - costList[statId];
                }
            }
        }

        //Spd
        if (statId == 2)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharSpd < maxPointsPerStat)
                {
                    uiCharSpd++;
                    uiPoints--;
                    uiPoints = uiPoints - costList[statId];
                }
            }
        }

        //def
        if (statId == 3)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharDef < maxPointsPerStat)
                {
                    uiCharDef++;
                    uiPoints--;
                    uiPoints = uiPoints - costList[statId];
                }
            }
        }

        pointCap = uiPoints;
        UiUpdate(UiGroups.CreationUi);
    }

    public void DecreaseStat(int statId)
    {
        // hp
        if (statId == 0)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharHp < maxPointsPerStat)
                {
                    uiCharHp--;
                    uiPoints++;
                    uiPoints = uiPoints + costList[statId];
                }
            }
        }

        //Str
        if (statId == 1)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharStr < maxPointsPerStat)
                {
                    uiCharStr--;
                    uiPoints++;
                    uiPoints = uiPoints + costList[statId];
                }
            }
        }

        //Spd
        if (statId == 2)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharSpd < maxPointsPerStat)
                {
                    uiCharSpd--;
                    uiPoints++;
                    uiPoints = uiPoints + costList[statId];
                }
            }
        }

        //def
        if (statId == 3)
        {
            if (pointCap > costList[statId])
            {
                if (uiCharDef < maxPointsPerStat)
                {
                    uiCharDef--;
                    uiPoints = uiPoints + costList[statId];
                }
            }
        }

        UiUpdate(UiGroups.CreationUi);
    }

    public void DoneBuying()
    {
        if (gm.turnHolder == "Red")
        {
            gm.redCanBuy = false;
            gm.turnHolder = "Blue";
            StartCoroutine(gm.PhaseEnd(GameManager.Phase.CreatePlayerCharacters, GameManager.Phase.DummyPhase));

        }
        else
        {
            gm.blueCanBuy = false;
            gm.turnHolder = "Red";
        }

        if (gm.blueCanBuy == false && gm.redCanBuy == false)
        {
            StartCoroutine(gm.PhaseEnd(GameManager.Phase.SelectingUnit, GameManager.Phase.CreatePlayerCharacters));
        }

    }

}
